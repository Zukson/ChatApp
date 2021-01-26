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
using System.Text.Json;
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
        public async Task JoinChatAsync(string connectionId,ChatUser user,ChatRoom chat)
        {
            await _chathub.Groups.AddToGroupAsync(connectionId, chat.Id);
         var chatDto=   await _data.ChatRooms.FindAsync(chat.Id);

            var userDto =await  _data.ChatUsers.FindAsync(user.Name);
            userDto.ChatRooms.Add(chatDto);
           chatDto.Users.Add(userDto);
           await _data.SaveChangesAsync();
        }

        public async Task<string>CreateChatAsync(List<string>connectionsId,List<ChatUser>users)
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
            AddChatToUsers(users, chat);
           var chatDto = _mapper.Map<ChatRoom, ChatRoomDto>(chat);
         await    _data.ChatRooms.AddAsync(chatDto);
            await _data.SaveChangesAsync();
            return chat.Id;
        }
        private void AddChatToUsers(List<ChatUser> users, ChatRoom Chat)
        {
            foreach (var user in users)
            {
                user.ChatRooms.Add(Chat);

            }
          
          


        }
        public async Task SendMessageAsync(Message message,string chatId)
        {

            var serializedMessage = JsonSerializer.Serialize(message);
            await _chathub.Clients.Group(chatId).SendAsync("receiveMessage", message);

        }
    }   
    
}
