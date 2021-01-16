using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class AuthenticateResult
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }

    }
}
