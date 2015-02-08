using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Schedule
    {
        public string student_id { get; set; }
        public string course_id { get; set; }
        public Nullable<int> score { get; set; }
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
