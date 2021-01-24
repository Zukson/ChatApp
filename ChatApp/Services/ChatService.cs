using AutoMapper;
using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Dto;
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
        private readonly IMapper _mapper;
        public ChatService(IHubContext<ChatHub> chathub,DataContext data,IMapper mapper)
        {
            _chathub = chathub;
            _data = data;
            _mapper = mapper;
        }
        public async Task JoinChat(string connectionId,ChatUser user,ChatRoom chat)
        {
            await _chathub.Groups.AddToGroupAsync(connectionId, chat.Id);
         var chatDto=   await _data.ChatRooms.FindAsync(chat.Id);

            var userDto = _mapper.Map<ChatUser, ChatUserDto>(user);
           chatDto.Users.Add(userDto);
           await _data.SaveChangesAsync();
        }

        public async Task<string>CreateChat(List<string>connectionsId,List<ChatUser>users)
        {

            var chat = new ChatRoom
            {
                Id = Guid.NewGuid().ToString(),
                Users = users,
                Messages = new List<Message>(),
                LastActivityDate = DateTime.UtcNow
            };
            foreach(var connectionId in connectionsId)
            {
              await   _chathub.Groups.AddToGroupAsync(connectionId, chat.Id);
            }
            var chatDto = _mapper.Map<ChatRoom, ChatRoomDto>(chat);
         await    _data.ChatRooms.AddAsync(chatDto);
            return chat.Id;
        }

    }

}
