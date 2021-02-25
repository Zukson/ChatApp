using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        }
        public bool AvatarExists(string fileName)
        {
            return File.Exists(Path.Combine(_avatarPath, fileName));
        }

       

        public   void  DeleteImage(string path)
        {
            FileInfo file = new FileInfo(path);
            
            if(file.Exists)
            {
                file.Delete();
            }

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
            string fileType = fileName.Split('.')[1];

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
