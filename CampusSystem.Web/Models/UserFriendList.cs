using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    public class UserFriendList
    {
        public string UserId { get; set; }
        public List<CampusSystem.Data.Models.User> FriendList { get; set; }
    }
}