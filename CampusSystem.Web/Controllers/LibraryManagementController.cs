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
    [CampusAuthorize(Roles="admin")]
    public class LibraryManagementController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: LibraryManagement
        public ActionResult Index()
        {
            GetRole();
            return View();
        }

        public ActionResult SearchUser(string userId)
        {
            GetRole();
            var libraryData = repo.GetLibraryInfo(userId);
            List<BookManagement> bm = new List<BookManagement>();
            foreach (var item in libraryData)
            {
                bm.Add(new BookManagement()
                {
                    Id = item.Id,
                    BookName = item.BookName,
                    CopyId = item.CopyId,
                    ExpireTime = item.ExpireTime,
                    Index = item.Index,
                    ISBN = item.ISBN,
                    LendTime = item.LendTime,
                    ReturnTime = item.ReturnTime,
                    Status = item.Status,
                    UserId = item.UserId
                });
            }
            ViewBag.UserId = userId;
            return PartialView("_searchUser", bm);
        }

        public ActionResult BookLend(string userId)
        {
            GetRole();
            ViewBag.UserId = userId;
            return View();
        }

        public ActionResult SearchCopy(string copyId, string userId)
        {
            GetRole();
            ViewBag.UserId = userId;
            ViewBag.CopyId = copyId;
            var search = repo.SearchCopy(copyId);
            if(search == null)
            {
                ViewBag.Error = "对不起，本书已经借出！";
                return View();
            }
            BookManagement bm = new BookManagement()
            {
                BookName = search.name,
                ISBN = search.isbn,
                Index = search.book_index
            };
            return View(bm);
        }

        public ActionResult ReturnBook(string id)
        {
            GetRole();
            repo.ReturnBook(id);
            return RedirectToAction("Index");
        }

        public ActionResult ConfirmLend(string copyId, string userId)
        {
            GetRole();
            repo.LendBook(copyId, userId);
            return RedirectToAction("Index");
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
            var currentUser = repo.GetUser(cuId);
            ViewBag.UserRole = currentUser.role;
        }
    }
}