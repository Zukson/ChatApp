using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public  IEnumerable<string> Errors { get; set; }

        public string JwtToken { get; set; }
        public  string RefreshToken { get; set; }


    }
}
