using ChatApp.Chat;
using ChatApp.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
   
    public class ChatHub : Hub
    {

        private readonly ChatDictionary _chatDctionary;
        public ChatHub(ChatDictionary chatDictionary)
        {
            _chatDctionary = chatDictionary;
        }
        public override async  Task OnConnectedAsync()
        {


            var connectionId = Context.ConnectionId;
          
            await Clients.Client(connectionId).
               SendAsync("receiveConnId", connectionId);



          
           

           
          
        }
        
       
      

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            _chatDctionary.RemoveByConnectionId(connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }


   public class PrivateMessage
    {
        public string MessageText { get; set; } 
        public string SenderName { get; set; }
    }

}
