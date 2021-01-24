using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dto
{
    public class ChatUserDto
    {
        [Key]
        public string Name { get; set; }
           
        public string AvatarPath { get; set; }

        ICollection<ChatRoomDto> ChatRooms { get; set; }

    }
}
