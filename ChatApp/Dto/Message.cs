using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dto
{
    public class Message
    {
        public string Text { get; set; }
        public string SenderId { get; set; }
        public DateTime SendDate { get; set; }
    }
}
