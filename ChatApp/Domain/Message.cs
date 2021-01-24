using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
   public class  Message
    {
        public string MessageId { get; set; }
        public string Text { get; set; }
        public string SenderName { get; set; }
        public DateTime SendDate { get; set; }
    }
}
