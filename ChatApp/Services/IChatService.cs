using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
   public interface IChatService
    {
        Task JoinChat(string connectionId, ChatUser user, ChatRoom chat);
        Task<string> CreateChat(List<string> connectionsId, List<ChatUser> users);
    }
}
