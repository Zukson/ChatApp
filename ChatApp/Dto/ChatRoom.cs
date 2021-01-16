using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dto
{
    public class ChatRoom
    {
        [Key]
        public string Id { get; set; }
        public List<Message> Messages { get; set; }
        public DateTime LastActivityDate { get; set; }

     

    }
}
