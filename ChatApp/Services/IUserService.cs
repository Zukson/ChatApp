using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    interface IUserService
    {
        public Task<string> GetImagePathAsync(string username);

        public Task SetImagePathAsync(string path, string username);
    }
}
