using System;
using System.Collections.Generic;

namespace CampusSystem.Data.Models
{
    public partial class Shuttle
    {
        public int shuttle_id { get; set; }
        public Nullable<System.TimeSpan> time { get; set; }
        public string start_location { get; set; }
        public string end_location { get; set; }
        public string weekday { get; set; }
    }
}
