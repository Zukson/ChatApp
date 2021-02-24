using ChatApp.Contracts;
using ChatApp.Extensions;
using ChatApp.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{

   
    public class UserController : Controller
    {
        private readonly IImageManager _fileManager;
        public UserController(IImageManager fileManager)
        {
            _fileManager = fileManager;
        }
        [HttpPost(ApiRoutes.User.PostUserAvatar)]
        public async Task<IActionResult>PostUserAvatar(IFormFile avatar)
        {
            var username = ClaimsExtensions.GetClaimValue(HttpContext.User.Claims, "name");

           await  _fileManager.SaveTemporaryAvatarAsync(username, avatar);

            return Ok();

           
        }
    }
}
