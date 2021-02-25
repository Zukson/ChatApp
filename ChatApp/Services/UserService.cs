using ChatApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _db;
        public UserService(DataContext db)
        {
            _db = db;
        }
        public async Task<string> GetImagePathAsync(string username)
        {
            var user = await _db.ChatUsers.FindAsync(username);

            return user.AvatarPath;

        }

        public async Task SetImagePathAsync(string path, string username)
        {
            var user = await _db.ChatUsers.FindAsync(username);

            user.AvatarPath = path;

            await _db.SaveChangesAsync();
        }
    }
}
