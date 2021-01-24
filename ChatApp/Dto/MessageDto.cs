using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dto
{
    public class MessageDto
    {
        [Key]
        public string MessageId { get; set; }
        public string Text { get; set; }
        public string SenderName{ get; set; }
        public DateTime SendDate { get; set; }


        [ForeignKey(nameof(SenderName))]
        public ChatUserDto chatUser { get; set; }
    }
}
