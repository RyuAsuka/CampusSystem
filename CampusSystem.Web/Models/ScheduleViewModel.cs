using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    /// <summary>
    /// 课程表视图模型
    /// </summary>
    public class ScheduleViewModel
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 星期
        /// </summary>
        public string Week { get; set; }
        
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }
}