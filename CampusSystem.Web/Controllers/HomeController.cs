using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CampusSystem.Data.Models;
using CampusSystem.Data;

namespace CampusSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: Home/Index or /
        public ActionResult Index()
        {
            string userId = GetUserId();
            var user = repo.GetUser(userId);
            if (userId != null)
            {
                ViewBag.UserRole = user.role;
            }
            return View();
        }

        /// <summary>
        /// 从Cookie中取得用户ID
        /// </summary>
        /// <returns>用户ID</returns>
        private string GetUserId()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                return ticket.Name;
            return null;
        }
    }
}