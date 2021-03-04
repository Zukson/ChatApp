using AutoMapper;
using ChatApp.Contracts;
using ChatApp.Contracts.Requests.Chat;
using ChatApp.Contracts.Responses.Chat;
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
using System.IdentityModel.Tokens.Jwt;
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

        [HttpGet(ApiRoutes.Chat.ConnectChats)]
        public async Task<IActionResult>ConnectChat([FromQuery] string connectionId)
        {
            string username = HttpContext.User.Claims.GetClaimValue("name");
          await  _chatService.JoinChatsAsync( username,connectionId);
            return Ok();
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


        public async Task<IActionResult> GetChatRooms()
        {
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

        [HttpPost(ApiRoutes.Chat.CreateChat)]

        public async Task<IActionResult>CreateChat([FromBody]CreateChatRequest createChatRequest)
        {
            //TODO check if user has had chatroom already with another user

             if(HttpContext.User.Claims.GetClaimValue("name")==createChatRequest.FriendName)
            {
                var badResponse = new { error = "You can not create chat with yourself" };
                return BadRequest(badResponse);
            }
            var user = await _data.ChatUsers.FindAsync(createChatRequest.FriendName);
                if(user==null)
            {
                var badResponse = new { error = "User doesnt exist" };
              return   BadRequest(badResponse);
            }

             
            string chatId= await _chatService.CreateChatAsync(createChatRequest.FriendName, HttpContext.User.Claims.GetClaimValue("name"), createChatRequest.ConnectionId);


            CreateChatResponse response = new CreateChatResponse { FriendName = createChatRequest.FriendName, ChatRoomId = chatId,LastActivityDate=DateTime.UtcNow };
            return Ok(response)      ;
        }
        
    }
}
