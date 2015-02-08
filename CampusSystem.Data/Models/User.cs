using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class User
    {
        public User()
        {
            this.Courses = new List<Course>();
            this.Groups = new List<Group>();
            this.Lends = new List<Lend>();
            this.Messages = new List<Message>();
            this.Schedules = new List<Schedule>();
            this.SendToes = new List<SendTo>();
            this.Groups1 = new List<Group>();
        }

        public string user_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string class_id { get; set; }
        public string role { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Lend> Lends { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<SendTo> SendToes { get; set; }
        public virtual ICollection<Group> Groups1 { get; set; }
    }
}
