using AutoMapper;
using ChatApp.Contracts.Requests;
using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Dto;
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
    [Route("Test")]
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

 [HttpPost]
        [Route("chat")]
        public async Task<IActionResult> JoinChat([FromBody] JoinChatRequest joinRequest)
        {
            var testuser = this.User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            string connectionId = joinRequest.ConnectionId;
          string  userName= HttpContext.User.Identity.Name;
         var userDto = await   _data.ChatUsers.FindAsync(userName);
            var chatDto = await _data.ChatRooms.FindAsync(joinRequest.ChatId);
            var user = _mapper.Map<ChatUserDto, ChatUser>(userDto);
            var chat = _mapper.Map<ChatRoomDto, ChatRoom>(chatDto);
        await    _chatService.JoinChat(connectionId, user, chat);
            return Ok();    

             
                
        }
    }
}
