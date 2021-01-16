using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Responses
{
    public class AuthSuccessResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
