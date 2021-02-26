using ChatApp.BackgroundServices;
using ChatApp.Contracts;
using ChatApp.Extensions;
using ChatApp.Files;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{

   
   [Authorize]
    public class UserController : Controller
    {
        private readonly IImageManager _fileManager;
        private readonly Channel<ImageMessage> _channel;
        public UserController(IImageManager fileManager,Channel<ImageMessage>channel)
        {
            _fileManager = fileManager;
            _channel = channel;

        }
        [HttpPost(ApiRoutes.User.PostUserAvatar)]
        public async Task<IActionResult>PostUserAvatar(IFormFile avatar)
        {
            try
            {
                var username = ClaimsExtensions.GetClaimValue(HttpContext.User.Claims, "name");

                var tmpPath = _fileManager.GetAvatarPath(avatar.FileName, username, ImageType.TemporaryAvatar);
                await _fileManager.SaveTemporaryAvatarAsync(tmpPath, avatar);
                ImageMessage imageMessage = new(username, avatar.FileName);
                await _channel.Writer.WriteAsync(imageMessage);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            };


            return Ok();

           
        }

        [HttpGet(ApiRoutes.User.GetUserAvatar)]

        public async Task<IActionResult>GetUserAvatar([FromQuery]string username,[FromServices]IUserService userService)
        {
          string path =await  userService.GetImagePathAsync(ClaimsExtensions.GetClaimValue(HttpContext.User.Claims,"name"));

            if (string.IsNullOrEmpty(path))
            {
                return BadRequest();
            }

          var stream=   _fileManager.GetImage(path);
            return new FileStreamResult(stream,"image/*");
        }
    }
}
