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
    public class CommonController : Controller,ISubject
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: Common
        public ActionResult UserName()
        {
            string userId = GetUserId();
            var user = repo.GetUser(userId);
            if (user != null)
            {
                ViewBag.UserName = user.name;
                ViewBag.userId = userId;
                ViewBag.UserRole = user.role;
            }
            return PartialView("_username");
        }

        /// <summary>
        /// 从Cookie中取得用户ID
        /// </summary>
        /// <returns>用户ID</returns>
        public string GetUserId()
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