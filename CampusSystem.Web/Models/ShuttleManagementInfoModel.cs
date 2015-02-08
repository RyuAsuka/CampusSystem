using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    /// <summary>
    /// 校车管理信息模型
    /// </summary>
    public class ShuttleManagementInfoModel
    {
        /// <summary>
        /// 校车班次ID
        /// </summary>
        public int ShuttleId { get; set; }

        /// <summary>
        /// 校车发车时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 校车起点站
        /// </summary>
        public string StartLocation { get; set; }

        /// <summary>
        /// 校车终点站
        /// </summary>
        public string EndLocation { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string Weekdays { get; set; }
    }
}