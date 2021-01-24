using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dto
{
    public class ChatRoomDto
    {
        [Key]
        public string Id { get; set; }
        public List<MessageDto> Messages { get; set; }
        public List<ChatUserDto> Users { get; set; }
        public DateTime LastActivityDate { get; set; }

     

    }
}
