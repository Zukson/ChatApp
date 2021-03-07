using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Contracts.Responses.Chat
{
    public class GetMessagesResponse
    {
       public List<MessageReponse> Messages { get; set; }
    }

    
}
