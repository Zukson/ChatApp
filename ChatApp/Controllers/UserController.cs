using ChatApp.BackgroundServices;
using ChatApp.Contracts;
using ChatApp.Contracts.Responses;
using ChatApp.Extensions;
using ChatApp.Files;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{

   
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IImageManager _fileManager;
        private readonly Channel<ImageMessage> _channel;
        private readonly IUserService _userService;
        public UserController(IImageManager fileManager,Channel<ImageMessage>channel,IUserService userService)
        {
            _fileManager = fileManager;
            _channel = channel;
            _userService = userService;

        }

        [HttpGet(ApiRoutes.User.GetUserThumbnail)]
        public async Task<IActionResult>GetUserThumbnail([FromQuery]string username, [FromServices] IUserService userService)
        {
            string path = await userService.GetImagePathAsync(username);

            if (string.IsNullOrEmpty(path))
            {
                return BadRequest();
            }

            var mime = path.Split('.').Last();
            var output = _fileManager.GetThumbnail(username, mime);

            return new FileContentResult(output, $"image/{mime}");


        }
        [HttpGet(ApiRoutes.User.GetUserInfo)]
        public async Task<IActionResult> GetUserInfo ()
        {
            var name = ClaimsExtensions.GetClaimValue(this.HttpContext.User.Claims, "name");

            var info = await _userService.GetUserInfoAsync(name);

            UserInfoResponse infoResponse = new UserInfoResponse { Username = info.Username, Email = info.Email };

            return Ok(infoResponse);

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

        public async Task<IActionResult>GetUserAvatar([FromServices]IUserService userService)
        {
          string path =await  userService.GetImagePathAsync(ClaimsExtensions.GetClaimValue(HttpContext.User.Claims,"name"));

            if (string.IsNullOrEmpty(path))
            {
                return BadRequest();
            }

            var mime = path.Split('.').Last();
          var output =   _fileManager.GetImage(path);

            if(output is null)
            {
                return BadRequest();
            }
            
            return new FileContentResult(_fileManager.GetImage(path),$"image/{mime}");
                }
    }
}
