using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Responses.Chat
{
    public class MessageReponse
    {
        public string Text { get; set; }
        public string SenderName { get; set; }
        public string  SendDate { get; set; }
    }
}
