using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Lend
    {
        public int lend_id { get; set; }
        public string user_id { get; set; }
        public string copy_id { get; set; }
        public System.DateTime lend_time { get; set; }
        public System.DateTime expire_time { get; set; }
        public Nullable<System.DateTime> return_time { get; set; }
        public virtual Copy Copy { get; set; }
        public virtual User User { get; set; }
    }
}
