using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusSystem.Data
{
    public enum Weekday { 周一, 周二, 周三, 周四, 周五 };

    /// <summary>
    /// 课程信息模型
    /// </summary>
    public class ScheduleModel
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public Weekday Week { get; set; }
        public string[] Times { get; set; }
        public string Place { get; set; }
    }
}
