using ChatApp.Data;
using ChatApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly DataContext _data;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(DataContext data,UserManager<IdentityUser> userManager)
        {
            _data = data;
            _userManager = userManager;
        }
        public async Task<AuthenticateResult> RegisterAsync(string password, string username, string email, IFormFile avatar)
        {
       var existedUser=    await _userManager.FindByEmailAsync(email);

            if(existedUser is not null)
            {
                return new AuthenticateResult { Errors = new string[] { "User already exist" } };
            }
            existedUser =await _userManager.FindByNameAsync(username);
            if(existedUser is not null)
            {
                return new AuthenticateResult { Errors = new string[] { "User already exist" } };
            }

            var newUserId = Guid.NewGuid();
            var user = new IdentityUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);
           


        }



    }
}
