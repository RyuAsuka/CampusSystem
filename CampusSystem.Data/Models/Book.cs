using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Book
    {
        public Book()
        {
            this.Copies = new List<Copy>();
        }

        public string isbn { get; set; }
        public string name { get; set; }
        public string book_index { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
    }
}
