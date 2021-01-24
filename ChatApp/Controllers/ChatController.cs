using ChatApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chathub;
        public ChatController(IHubContext<ChatHub> chathub)
        {
            _chathub = chathub;
        }
        public IActionResult Index()
        {
           
        }

        public async Task JoinChat(string id)
        {

        }
    }
}
