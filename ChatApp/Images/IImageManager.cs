using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Files
{
  public  interface  IImageManager
    {
        FileStream GetImage(string path);

        Task SaveImageAsync(string temporaryPath,string path,int size);
       

     void  DeleteImage(string path);

        Task SaveTemporaryAvatarAsync(string path,IFormFile avatar);


        string GetAvatarPath(string fileName, string username, ImageType imageType);







    }
}
