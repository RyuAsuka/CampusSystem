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
    public class ShuttleManagementController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: ShuttleManagement
        public ActionResult Index()
        {
            GetRole();
            var allShuttleInfo = repo.GetAllShuttleInfo();
            List<ShuttleManagementInfoModel> managementList = new List<ShuttleManagementInfoModel>();
            foreach (var item in allShuttleInfo)
            {
                managementList.Add(new ShuttleManagementInfoModel
                {
                    ShuttleId = item.shuttle_id,
                    Time = item.time.ToString(),
                    StartLocation = item.start_location,
                    EndLocation = item.end_location,
                    Weekdays = item.weekday
                });
            }
            return View(managementList);
        }

        public ActionResult Create()
        {
            GetRole();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ShuttleManagementInfoModel model)
        {
            GetRole();
            if (model.StartLocation == model.EndLocation)
            {
                ViewBag.ErrorMessage = "起点和终点不能相同！";
                return View(model);
            }
            if (model.Time == "" || model.Weekdays == "")
            {
                ViewBag.ErrorMessage = "请检查是否有空项！";
                return View(model);
            }
            else
            {
                repo.CreateShuttle(model.Time, model.StartLocation, model.EndLocation, model.Weekdays);
                //TODO:发送消息逻辑
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            GetRole();
            var itemToEdit = repo.GetShuttleInfo(id);
            ShuttleManagementInfoModel model = new ShuttleManagementInfoModel()
            {
                ShuttleId = itemToEdit.shuttle_id,
                Time = itemToEdit.time.ToString(),
                Weekdays = itemToEdit.weekday,
                EndLocation = itemToEdit.end_location,
                StartLocation = itemToEdit.start_location
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ShuttleManagementInfoModel model)
        {
            GetRole();
            //model.ShuttleId = int.Parse(Request.Form["id"]);
            if (model.StartLocation == model.EndLocation)
            {
                ViewBag.ErrorMessage = "起点和终点不能相同！";
                return View(model);
            }
            if (model.Time == "" || model.Weekdays == "")
            {
                ViewBag.ErrorMessage = "请检查是否有空项！";
                return View(model);
            }
            else
            {
                repo.EditShuttle(model.ShuttleId, model.Time, model.StartLocation, model.EndLocation, model.Weekdays);
                //TODO:发送消息逻辑
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            GetRole();
            repo.DeleteShuttle(id);
            ViewBag.Id = id;
            //TODO:发送消息逻辑
            return View();
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