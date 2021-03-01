using AutoMapper;
using ChatApp.Contracts;
using ChatApp.Contracts.Requests.Chat;
using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Dto;
using ChatApp.Extensions;
using ChatApp.Hubs;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly DataContext _data;
        private readonly IMapper _mapper;
        public ChatController(IChatService chatService,IHubContext<ChatHub>chatHub, DataContext data,IMapper mapper)
        {
           _chatService = chatService;
            _chatHub = chatHub;
            _data = data;
            _mapper = mapper;
        }

 [HttpPost(ApiRoutes.Chat.JoinChat)]
       
        public async Task<IActionResult> JoinChat([FromBody] JoinChatRequest joinRequest)
        {
           
            string connectionId = joinRequest.ConnectionId;
            var username = ClaimsExtensions.GetClaimValue(HttpContext.User.Claims,"name");
         var userDto = await   _data.ChatUsers.FindAsync(username);
            var chatDto = await _data.ChatRooms.FindAsync(joinRequest.ChatId);
            var user = _mapper.Map<ChatUserDto, ChatUser>(userDto);
            var chat = _mapper.Map<ChatRoomDto, ChatRoom>(chatDto);
        await    _chatService.JoinChatAsync(connectionId, user, chat);
            return Ok();    

             
                
        }

        
        [HttpPost(ApiRoutes.Chat.SendMessage)]
        public async Task<IActionResult> SendMessage([FromBody]SendMessageRequest messageRequest)
        {
            var name = ClaimsExtensions.GetClaimValue(HttpContext.User.Claims, "name");
            var message = new Message { MessageId = Guid.NewGuid().ToString(), SenderName = name, SendDate = DateTime.UtcNow, Text = messageRequest.Message };
            await _chatService.SendMessageAsync(message, messageRequest.ChatId);
            return Ok();
        }
    }
}
