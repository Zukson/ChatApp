
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChatApp.Contracts.Responses.Chat
{
   public    class GetChatRoomsResponse
    {
        public List<SingleChatRoom> Chats { get; set; }

    }

  public class SingleChatRoom
    {
        public string ChatRoomId { get; set; }

        public string FriendName { get; set; }
        public DateTime LastActivityDate { get; set; }
    }

}