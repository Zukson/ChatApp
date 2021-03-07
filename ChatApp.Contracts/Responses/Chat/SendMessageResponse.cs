using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Responses.Chat
{
   public class SendMessageResponse
    {
        public string Text { get; set; }
        public string SenderName { get; set; }
        public DateTime SendDate { get; set; }
    }
}
