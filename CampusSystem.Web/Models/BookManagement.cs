using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    public class BookManagement
    {
        public int Id { get; set; }
        public string CopyId { get; set; }
        public string BookName { get; set; }
        public string UserId { get; set; }
        public string ISBN { get; set; }
        public string Index { get; set; }
        public DateTime LendTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public Nullable<DateTime> ReturnTime { get; set; }
        public bool Status { get; set; }
    }
}