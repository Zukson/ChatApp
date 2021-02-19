using ChatApp.Contracts;
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
        
        [HttpPost(ApiRoutes.User.PostUserAvatar)]
        public async Task<IActionResult>PostUserAvatar(IFormFile avatar)
        {
            return Ok();
        }
    }
}
