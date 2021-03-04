using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Responses.Chat
{
    public class CreateChatResponse
    {
        public string ChatRoomId { get; set; }

        public string FriendName { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
