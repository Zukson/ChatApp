using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Requests.Chat
{
   public class SendMessageRequest
    {
        public string ChatId { get; set; }
        public string Message { get; set; }
    }
}
