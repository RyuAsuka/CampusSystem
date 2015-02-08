using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    /// <summary>
    /// 课程信息视图模型
    /// </summary>
    public class CourseViewModel
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        public string Teacher { get; set; }
        
        /// <summary>
        /// 学分
        /// </summary>
        public int Creadits { get; set; }

        /// <summary>
        /// 考试日期
        /// </summary>
        public string ExamTime { get; set; }

        /// <summary>
        /// 上课地点
        /// </summary>
        public string Place { get; set; }
    }
}