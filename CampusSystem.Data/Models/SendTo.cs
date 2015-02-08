using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class SendTo
    {
        public int message_id { get; set; }
        public string receiver { get; set; }
        public bool is_sent { get; set; }
        public virtual Message Message { get; set; }
        public virtual User User { get; set; }
    }
}
