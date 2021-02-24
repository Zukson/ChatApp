using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Files
{
    public class FileManager : IFileManager
    {
        private readonly FileSettings _fileSsettings;
        private readonly IWebHostEnvironment _enviroment;

        private readonly string _tmpAvatarPath;
        private readonly string _avatarPath;
        private readonly string _thumbnailPath;
        public FileManager(FileSettings fileSettings,IWebHostEnvironment environment)
        {
            _fileSsettings = fileSettings;
            _enviroment = environment;
            _tmpAvatarPath = Path.Combine(_enviroment.ContentRootPath,"Files", _fileSsettings.TemporaryAvatarPath);
            _avatarPath = Path.Combine(_enviroment.ContentRootPath, "Files", _fileSsettings.AvatarPath);
            _thumbnailPath = Path.Combine(_enviroment.ContentRootPath, "Files", _fileSsettings.ThumbnailPath);
        }
        public bool AvatarExists(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAvatarAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task SaveAvatarAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public  async Task SaveTemporaryAvatarAsync(string username, IFormFile avatar)
        {
            var path = GetTemporaryAvatarPath(username);
            string fileType = avatar.FileName.Split('.')[1];
            path = path +'.'+ fileType;
            using FileStream fs = new FileStream(path,FileMode.Create,FileAccess.Write);
            {
                await avatar.CopyToAsync(fs);
                
            }


    
        }

        private string   GetTemporaryAvatarPath(string username)
        {
            return Path.Combine(_tmpAvatarPath, $"tmp_{username}");
        }
    }
}
