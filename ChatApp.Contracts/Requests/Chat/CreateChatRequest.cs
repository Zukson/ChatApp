using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Requests.Chat
{
   public class CreateChatRequest
    {
        public string ConnectionId { get; set; }

        public string FriendName { get; set; }
    }
}
