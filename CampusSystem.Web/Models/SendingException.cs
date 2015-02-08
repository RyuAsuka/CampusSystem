using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    public class SendingException : Exception
    {
        public SendingException(string msg):base(msg)
        {
            
        }
    }
}