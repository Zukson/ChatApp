using AutoMapper;
using ChatApp.Chat;
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

        [HttpGet(ApiRoutes.Chat.GetChatUsers)]

        public async Task<IActionResult>GetChatUsers([FromQuery]string chatRoomId)
        {
          var result = await  _chatService.GetChatRoomsUsersAsync(chatRoomId);

            return Ok(result);
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

        [HttpGet(ApiRoutes.Chat.GetChats)]
       
        public async Task<IActionResult> GetChatRooms()
        {
            string username = HttpContext.User.Claims.GetClaimValue("name");

            var response = await _chatService.GetChatsAsync(username);
            return Ok(response);
        }

        public async Task<IActionResult > GetMessagesForChatRoom(string chatRoomId)
        {
            var messages =await  _chatService.GetMessagesAsync(chatRoomId);

            if(messages is null)
            {
                return BadRequest();
            }
            
            var messagesResponse = messages.Select(x => new MessageReponse
           { 
                SenderName = x.SenderName,
                SendDate = x.SendDate, 
                Text = x.Text 
           });

            return Ok(messagesResponse);
        }


        [HttpPost(ApiRoutes.Chat.SendMessage)]
        public async Task<IActionResult> SendMessage([FromBody]SendMessageRequest messageRequest)
        {
            var name = ClaimsExtensions.GetClaimValue(HttpContext.User.Claims, "name");
            var message = new Message { MessageId = Guid.NewGuid().ToString(), SenderName = name, SendDate = DateTime.UtcNow, Text = messageRequest.Message };
            await _chatService.SendMessageAsync(message, messageRequest.ChatId);

            var response = new SendMessageResponse { SenderName = message.SenderName, SendDate = message.SendDate, Text = message.Text };
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Chat.CreateChat)]

        public async Task<IActionResult>CreateChat([FromServices]ICreateChatValidator validator,[FromBody]CreateChatRequest createChatRequest)
        {
            

            var username = HttpContext.User.Claims.GetClaimValue("name");

        var result  = await validator.ValidateRequest(username, createChatRequest.FriendName);

            if(result.IsValid == false)
            {
                var failedResponse = new FailedCreateChatResponse { Error = result.Error };
                return BadRequest(failedResponse);

            }
             
            string chatId= await _chatService.CreateChatAsync(createChatRequest.FriendName, username, createChatRequest.ConnectionId);


            CreateChatResponse response = new CreateChatResponse { FriendName = createChatRequest.FriendName, ChatRoomId = chatId,LastActivityDate=DateTime.UtcNow };
            return Ok(response)      ;
        }
        
    }
}
