using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Chat
{
    public class ClientMessage
    {
        public string ChatId { get; set; }
        public string Text { get; set; }
        public string SenderName { get; set; }
        public string SendDate { get; set; }
    }
}
