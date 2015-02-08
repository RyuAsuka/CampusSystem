using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    public class UserModel
    {
        /// <summary>
        /// 获取或设置用户账号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置用户是否自动登录
        /// </summary>
        public bool AutoLogin { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }
    }
}