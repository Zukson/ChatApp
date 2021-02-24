using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Files
{
  public  interface  IFileManager
    {
        bool AvatarExists(string fileName);

        Task SaveAvatarAsync(string fileName);

        Task DeleteAvatarAsync(string fileName);

        Task SaveTemporaryAvatarAsync(string fileName,IFormFile avatar);

       




        


    }
}
