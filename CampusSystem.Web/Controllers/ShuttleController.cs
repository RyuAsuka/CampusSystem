using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampusSystem.Data;
using CampusSystem.Data.Models;
using CampusSystem.Web.Models;
using System.Web.Security;

namespace CampusSystem.Web.Controllers
{
    public class ShuttleController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: Shuttle
        public ActionResult Index()
        {
            GetRole();
            return View();
        }

        public ActionResult Search(string weekday, string startLocation, string endLocation)
        {
            var shuttles = repo.SearchShuttle(weekday, startLocation, endLocation);
            return PartialView("_Search", shuttles);
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

        public void GetRole()
        {
            string cuId = GetUserId();
            if (cuId != null)
            {
                var currentUser = repo.GetUser(cuId);
                ViewBag.UserRole = currentUser.role;
            }
        }
    }
}