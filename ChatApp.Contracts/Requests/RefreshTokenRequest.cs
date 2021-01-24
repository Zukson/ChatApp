using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Requests
{
  public  class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string JwtToken { get; set; }
    }
}
