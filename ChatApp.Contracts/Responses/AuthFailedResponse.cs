using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }

    }
}
