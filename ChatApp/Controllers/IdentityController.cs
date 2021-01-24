using AutoMapper;
using ChatApp.Contracts;
using ChatApp.Contracts.Requests;
using ChatApp.Contracts.Responses;
using ChatApp.Domain;
using ChatApp.Dto;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    public class IdentityController :Controller
    {
       
       
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService )
        {
            _identityService = identityService;
        
        }
        [HttpPost(ApiRoutes.Identity.Register)]
       public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {

          
            var result = await _identityService.RegisterAsync(request.Password, request.Username, request.Email);

            if(!result.IsSuccess)
            {
                var resultResponse = new AuthFailedResponse { Errors = result.Errors };
                return   BadRequest(resultResponse);
            }

            else
            {
                var resultResponse = new AuthSuccessResponse { JwtToken = result.JwtToken, RefreshToken = result.RefreshToken };
                return  Ok(resultResponse);
            }
            


        }
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult>Login([FromBody]UserLoginRequest request)
        {
        var result =    await  _identityService.LoginAsync( request.Email, request.Password);

            if (!result.IsSuccess)
            {


                return BadRequest(new AuthFailedResponse { Errors = result.Errors });
            }

            else
            {
                var resultResponse = new AuthSuccessResponse { JwtToken = result.JwtToken, RefreshToken = result.RefreshToken };
                return  Ok(resultResponse);
            }

        }
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest tokenRequest)
        {

            var response = await _identityService.RefreshTokenAsync(tokenRequest.JwtToken, tokenRequest.RefreshToken);
            if(response.IsSuccess)
            {
                return  Ok(new AuthSuccessResponse { RefreshToken = response.RefreshToken, JwtToken = response.JwtToken });

            }

            else
            {
                return BadRequest();
            }
        }
    }
}
