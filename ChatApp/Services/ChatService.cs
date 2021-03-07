using AutoMapper;
using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Dto;
using ChatApp.Chat;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ChatApp.Contracts.Responses.Chat;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _chathub;
        private readonly DataContext _data;
        private readonly IMapper _mapper;
        private readonly ChatDictionary _chatDictionary;

        
        public ChatService(IHubContext<ChatHub> chathub,DataContext data,IMapper mapper,ChatDictionary chatDictionary)
        {
            _chathub = chathub;
            _data = data;
            _mapper = mapper;
            _chatDictionary = chatDictionary;
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

        public async Task<GetChatRoomsResponse> GetChatsAsync(string username)
        {
            List<ChatRoomDto> chatRoomDtos = await _data.ChatRooms.Include(x => x.Users).Where(x => x.Users.Any(x => x.Name == username)).ToListAsync();


            var chatRoomsRepsonse = chatRoomDtos.Select(x => new ChatRoomResponse
            {
                ChatRoomId = x.Id,
                FriendName = x.Users.FirstOrDefault(x => x.Name != username).Name
                ,
                LastActivityDate = x.LastActivityDate
            });

            GetChatRoomsResponse GetChatRoomsResponse = new GetChatRoomsResponse { Chats = chatRoomsRepsonse.ToList() };
            return GetChatRoomsResponse;
        }
        public async Task JoinChatsAsync(string username,string connectionId)
        {
            var user = await _data.ChatUsers.Include(x => x.ChatRooms).FirstOrDefaultAsync(x => x.Name == username);

                foreach(var chatRoom in user.ChatRooms)
            {
                await _chathub.Groups.AddToGroupAsync(connectionId, chatRoom.Id);
            }

            _chatDictionary.Add(username, connectionId);
        }
        public async Task<string>CreateChatAsync(string  friendname,string username, string connectionId)
        {

            //ChatUserDto user = await _data.ChatUsers.FindAsync(username);

           

            ChatUserDto user = await _data.ChatUsers.Include(x => x.ChatRooms).FirstOrDefaultAsync(x=>x.Name==username);
            ChatUserDto friend = await _data.ChatUsers.Include(x => x.ChatRooms).FirstOrDefaultAsync(x=>x.Name==friendname);
            List<ChatUserDto> users = new List<ChatUserDto> { user, friend };
            var chat = new ChatRoomDto
            {
                Id = Guid.NewGuid().ToString(),
                Users = users,
                Messages = new List<MessageDto>(),
                LastActivityDate = DateTime.UtcNow
            };
            
            AddChatToUsers(users, chat);
          
         await    _data.ChatRooms.AddAsync(chat);
          var friendConId = _chatDictionary.GetConnectionId(friendname);

            if(friendConId is not  null)
            {
                CreatedChat(friendConId, chat.Id, username);
                AddUsersToGroup(new[] { connectionId,friendConId},chat.Id);

                var chatCreatedMessage = new { chatRoomId = chat.Id, userName = user.Name };
               await  _chathub.Clients.Client(friendConId).SendAsync("chatCreated",chatCreatedMessage);
            }

            else
            {
                try
                {

                AddUsersToGroup(new[] { connectionId }, chat.Id);
                }
                catch(Exception ex)
                {
                    var message = ex.ToString();
                }
            }
           
            
            await _data.SaveChangesAsync();

            var usedasdasdr = await _data.ChatUsers.Include(x => x.ChatRooms).FirstOrDefaultAsync(x => x.Name == username);



            return chat.Id;
        }
        private void AddChatToUsers(List<ChatUserDto> users, ChatRoomDto Chat)
        {
            foreach (var user in users)
            {
                user.ChatRooms.Add(Chat);

            }
          
          


        }

        private void AddUsersToGroup(string[] connectionIds,string chatroomId )
        {
            foreach(string conId in connectionIds)
            {
                _chathub.Groups.AddToGroupAsync(conId,chatroomId);
            }
        }


        public async Task ConnectUser(string connectionId,string username)
        {
          var user = await   _data.ChatUsers.FindAsync(username);


            foreach(var chatRoom in  user.ChatRooms)
            {
                await _chathub.Groups.AddToGroupAsync(connectionId, chatRoom.Id);
            }
        }
        public async Task SendMessageAsync(Message message,string chatId)
        {

          var chatRoom = await   _data.ChatRooms.FindAsync(chatId);

            var messageDto = _mapper.Map<MessageDto>(message);
            chatRoom.Messages.Add(messageDto);
            chatRoom.LastActivityDate = messageDto.SendDate;
            await _data.SaveChangesAsync();
            var serializedMessage = JsonSerializer.Serialize(message);
            await _chathub.Clients.Group(chatId).SendAsync("receiveMessage", message);

        }

        private void CreatedChat(string connectionId,string roomId,string username)
        {
            var message = new { roomId, username };
            var serializedMessage = JsonSerializer.Serialize(message);

            _chathub.Clients.Client(connectionId).SendAsync("createdChat", serializedMessage);
        }

        public  async Task<List<Message>> GetMessagesAsync(string chatRoomId)
        {
          var chatRoom = await  _data.ChatRooms.FindAsync(chatRoomId);

            var messagesDto = chatRoom.Messages;

            var messages = _mapper.Map<List<Message>>(messagesDto);

            return messages;
        }
    }   
    
}
