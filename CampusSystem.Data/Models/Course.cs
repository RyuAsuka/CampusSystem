using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Course
    {
        public Course()
        {
            this.Schedules = new List<Schedule>();
        }

        public string course_id { get; set; }
        public string name { get; set; }
        public string teacher_id { get; set; }
        public string time { get; set; }
        public int credits { get; set; }
        public System.DateTime exam_time { get; set; }
        public string place { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
