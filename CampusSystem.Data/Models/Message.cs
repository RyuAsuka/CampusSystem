using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Message
    {
        public Message()
        {
            this.SendToes = new List<SendTo>();
        }

        public int message_id { get; set; }
        public string sender { get; set; }
        public System.DateTime send_time { get; set; }
        public string message_content { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<SendTo> SendToes { get; set; }
    }
}
