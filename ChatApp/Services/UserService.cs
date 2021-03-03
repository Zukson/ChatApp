using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Dto;
using Microsoft.AspNetCore.Identity;
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
       private readonly UserManager<IdentityUser> _userManager;
        public UserService(DataContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task CreateAsync(string username)
        {
            await _db.ChatUsers.AddAsync(new ChatUserDto { Name = username });
            await _db.SaveChangesAsync();
        }

        

        public async Task<string> GetImagePathAsync(string username)
        {
            var user = await _db.ChatUsers.FindAsync(username);

            return user.AvatarPath;

        }

        public async Task<UserInfo> GetUserInfoAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            UserInfo userInfo = new UserInfo { Email = user.Email, Username = user.UserName };

            return userInfo;

        }

        public async Task SetImagePathAsync(string path, string username)
        {
            var user = await _db.ChatUsers.FindAsync(username);

            user.AvatarPath = path;

            await _db.SaveChangesAsync();
        }
    }
}
