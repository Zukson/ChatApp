using ChatApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Chat
{
    public class CreateChatValidator :ICreateChatValidator
    {

        private readonly DataContext _data;
        public CreateChatValidator(DataContext data)
        {
            _data = data;
        }
        public async  Task<CreateChatValidationResult> ValidateRequest(string username,string friendName)
        {
            
            if(!await UserExists(friendName))
                {
                var result = new CreateChatValidationResult { Error = "User doesnt exist" ,IsValid=false};
                return result;
                }
                
            if ( await HasAlreadyChatWithUser(username,friendName))
            {
                var result = new CreateChatValidationResult { Error = "You have already chat with requested User" ,IsValid=false};
                return result;
            }


            var successResult = new CreateChatValidationResult { IsValid = true };
            return successResult; 
        }

        private async Task<bool> HasAlreadyChatWithUser(string username,string friendName)
        {
            var user = await _data.ChatUsers.Include(x => x.ChatRooms).FirstOrDefaultAsync(x => x.Name == username);
            bool hasChat = false;
            var chatRooms = user.ChatRooms.ToList();
            int i = 0;
            while(i<chatRooms.Count()&&hasChat==false)
            {
                int j = 0;
                var chatRoom = chatRooms.ElementAt(i);

                while (j < chatRoom.Users.Count() && hasChat == false)
                {
                    if (chatRoom.Users.ElementAt(j).Name == friendName)
                    {
                        hasChat = true;
                    }
                    j++;
                }

                i++;
            }
            return hasChat;
            
        
        }

        private async Task<bool>UserExists(string friendName)
        {
           var user = await _data.ChatUsers.FindAsync(friendName);

            if(user is null)
            {
                return false;
            }

            return true;
        }
    }
}
