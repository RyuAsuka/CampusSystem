using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSystem.Web.Models
{
    /// <summary>
    /// 已借出图书的模型
    /// </summary>
    public class BooksLent
    {
        /// <summary>
        /// 借书ID
        /// </summary>
        public int LentId { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime BorrowTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 归还时间，可空
        /// </summary>
        public Nullable<DateTime> ReturnTime { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
    }
}