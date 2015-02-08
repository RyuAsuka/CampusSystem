using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Class
    {
        public Class()
        {
            this.Users = new List<User>();
        }

        public string class_id { get; set; }
        public string name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
