using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
public class ChatRoom
    {
        public string Id { get; set; }
        public List<Message> Messages { get; set; }
        public List<ChatUser> Users { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
