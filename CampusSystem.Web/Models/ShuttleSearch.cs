﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    /// <summary>
    /// 校车班次查询视图模型
    /// </summary>
    public class ShuttleSearch
    {
        /// <summary>
        /// 星期
        /// </summary>
        public string Weekday { get; set; }
        
        /// <summary>
        /// 起点
        /// </summary>
        public string StartLocation { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public string EndLocation { get; set; }
    }
}