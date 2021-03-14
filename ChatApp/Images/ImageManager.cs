using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Files
{
    public class ImageManager : IImageManager
    {
        
       
        private readonly ImageSettings _fileSsettings;
        private readonly IWebHostEnvironment _enviroment;

       
        private readonly string _tmpAvatarPath;
        private readonly string _avatarPath;
        private readonly string _thumbnailPath;
        const string baseDirectory = "Images";
      
        public ImageManager(ImageSettings fileSettings,IWebHostEnvironment environment)
        {
            _fileSsettings = fileSettings;
            _enviroment = environment;
            _tmpAvatarPath = Path.Combine(_enviroment.ContentRootPath, baseDirectory, _fileSsettings.TemporaryAvatarPath);
            _avatarPath = Path.Combine(_enviroment.ContentRootPath, baseDirectory, _fileSsettings.AvatarPath);
            _thumbnailPath = Path.Combine(_enviroment.ContentRootPath, baseDirectory, _fileSsettings.ThumbnailPath);
            CreateFolders();


        }
        private void CreateFolders()

        {
            bool exists = System.IO.Directory.Exists((_tmpAvatarPath));

            if (!exists)
                System.IO.Directory.CreateDirectory((_tmpAvatarPath));

            exists = System.IO.Directory.Exists((_avatarPath));
            if (!exists)
                System.IO.Directory.CreateDirectory((_avatarPath));


            exists = System.IO.Directory.Exists((_thumbnailPath));
            if (!exists)
                System.IO.Directory.CreateDirectory((_thumbnailPath));


        }

        private bool ImageExists(string path)
        {
            return File.Exists(path);
        }

       

        public   void  DeleteImage(string path)
        {
            FileInfo file = new FileInfo(path);
            
            if(file.Exists)
            {
                file.Delete();
            }

        }

        public byte[]GetThumbnail(string username,string mime)
        {
            string path = Path.Combine(_thumbnailPath, $"{username}.{mime}");
            if (ImageExists(path))
            {

                byte[] b = System.IO.File.ReadAllBytes(path);

                return b;

            }

            return null;



        }
        public byte[] GetImage(string path)
        {
            if(ImageExists(path))
            {
                
                    byte[] b = System.IO.File.ReadAllBytes(path);

                    return b;
               
            }

            return null;
        }

       
       
        public  async Task SaveImageAsync(string temporaryPath,string path,int size)
        {
            using Image image = Image.Load(temporaryPath)
;
            image.Mutate(x => x.Resize(size, size));
            await image.SaveAsync(path);
        }

     
        public  async Task SaveTemporaryAvatarAsync(string path, IFormFile avatar)
        {
        
         
          
           
            using FileStream fs = new FileStream(path,FileMode.Create,FileAccess.Write);
            {
                await avatar.CopyToAsync(fs);
                
            }


    
        }

       

      public  string   GetAvatarPath(string fileName,string username,ImageType imageType)
        {
            string fileType = fileName.Split('.').Last();

            var pathByType = imageType switch
            {
                ImageType.Avatar => _avatarPath,
                ImageType.TemporaryAvatar => _tmpAvatarPath,
                ImageType.Thumbnail => _thumbnailPath
            };
            return Path.Combine(pathByType,$"{username}.{fileType}");
        }

        

       
    }
}
