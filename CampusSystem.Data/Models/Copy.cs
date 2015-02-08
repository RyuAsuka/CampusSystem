using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Copy
    {
        public Copy()
        {
            this.Lends = new List<Lend>();
        }

        public string copy_id { get; set; }
        public string isbn { get; set; }
        public bool status { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<Lend> Lends { get; set; }
    }
}
