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
    [CampusAuthorize(Roles="student, teacher")]
    public class LibraryController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());
        
        // GET: Library
        public ActionResult Index()
        {
            string userId = GetUserId();
            var currentUser = repo.GetUser(userId);
            ViewBag.UserRole = currentUser.role;
            var booksLentData = repo.GetBooksLent(userId);
            List<BooksLent> booksLentList = new List<BooksLent>();
            foreach(var item in booksLentData)
            {
                if(item.return_time == null)
                    booksLentList.Add(new BooksLent() {LentId = item.lend_id, BookName = repo.GetBookNameByCopyId(item.copy_id), BorrowTime = item.lend_time, ExpireTime = item.expire_time, IsChecked = false });
            }
            return View(booksLentList);
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
        
        [HttpPost]
        public ActionResult ContinueAll()
        {
            string userId = GetUserId();
            var booksLentData = repo.GetBooksLent(userId);
            foreach(var item in booksLentData)
            {
                repo.ContinueLend(item.lend_id);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ContinueChecked(FormCollection collection)
        {
            string str = collection["checkitem"];
            string[] strContinue = str.Split(',');
            foreach(var i in strContinue)
            {
                if(i != "false")
                {
                    repo.ContinueLend(int.Parse(i));
                }
            }
            return RedirectToAction("Index");
        }
    }
}