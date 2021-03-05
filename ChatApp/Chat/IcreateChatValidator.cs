using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Chat
{
  public  interface ICreateChatValidator
    {

        Task<CreateChatValidationResult> ValidateRequest(string username, string friendName);
    }
}
