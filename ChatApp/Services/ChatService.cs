using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _chathub;
        private readonly DataContext _data;
        public ChatService(IHubContext<ChatHub> chathub,DataContext data)
        {
            _chathub = chathub;
            _data = data;
        }
        public async Task JoinChat(string connectionId,ChatUser user,ChatRoom chat)
        {
            await _chathub.Groups.AddToGroupAsync(connectionId, chat.Id);
         var chatDb=   await _data.ChatRooms.FindAsync(chat.Id);

            if(chatDb is null)
            {
                _data.ChatRooms.AddAsync()
            }
        }

    }

}
