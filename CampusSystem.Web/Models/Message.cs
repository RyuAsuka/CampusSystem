﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    public class Message
    {
        public string Sender { get; set; }
        public DateTime SendTime { get; set; }
        public string MessageContent { get; set; }
    }
}