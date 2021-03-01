using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
   public interface IChatService
    {
        Task JoinChatAsync(string connectionId, ChatUser user, ChatRoom chat);
        Task<string> CreateChatAsync(string friendname,string username,string connectionId);
        Task SendMessageAsync(Message message, string chatId);
    }
}
