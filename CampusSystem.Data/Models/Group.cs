using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Group
    {
        public Group()
        {
            this.Users = new List<User>();
        }

        public int group_id { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
