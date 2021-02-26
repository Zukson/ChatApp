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
        public override async  Task OnConnectedAsync()
        {
            string connId = Context.ConnectionId;
           
           await Clients.Client(Context.ConnectionId).
                SendAsync("receiveConnId", connId);
            await Task.Delay(2000);
            await TestMessage();
          
        }
     
        public async Task TestMessage()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("receiveMessage", "siema");
        }
        public async Task SendMessage(string chatId,string senderName,string message)
        {
            var privateMessage = new PrivateMessage { MessageText = message, SenderName = senderName };
            var serializedMessage = JsonSerializer.Serialize(privateMessage);
            await Clients.Group(chatId).SendAsync("receiveMessage", serializedMessage);
        }
    }


   public class PrivateMessage
    {
        public string MessageText { get; set; } 
        public string SenderName { get; set; }
    }

}
