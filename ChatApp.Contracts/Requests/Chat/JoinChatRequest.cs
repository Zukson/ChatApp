using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Requests.Chat
{
  public   class JoinChatRequest
    {
     public   string ConnectionId { get; set; }
     public   string ChatId { get; set; }
    }
}
