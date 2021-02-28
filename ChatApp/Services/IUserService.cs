using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
   public  interface IUserService
    {

      
        public Task<string> GetImagePathAsync(string username);

        public Task SetImagePathAsync(string path, string username);

        public Task CreateAsync(string username);
         public Task<UserInfo> GetUserInfoAsync(string username);
    }
}
