using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CampusSystem.Data;
using CampusSystem.Data.Models;
using CampusSystem.Web.Models;
using System.Text.RegularExpressions;

namespace CampusSystem.Web.Controllers
{
    [CampusAuthorize(Roles = "student, teacher, admin")]
    public class MessageController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: Message/SendMessage
        public ActionResult SendMessage()
        {
            GetRole();
            string userId = GetUserId();

            UserFriendList fl = new UserFriendList();
            fl.UserId = userId;
            fl.FriendList = repo.GetFriendList(userId);
            ViewBag.Friends = fl;

            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(CampusSystem.Web.Models.Message message)
        {
            string userId = GetUserId();
            Regex regex = new Regex(@"^\d+$");
            List<string> targets = new List<string>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (regex.IsMatch(key))
                {
                    if (Request.Form[key].Contains("true"))
                        targets.Add(key);
                }
            }
            message.MessageContent = Request.Form["MessageContent"];
            message.Sender = userId;
            message.SendTime = DateTime.Now;
            try
            {
                if (targets.Count == 0)
                    throw new SendingException("请选择发送到的对象！");
                if (string.IsNullOrEmpty(message.MessageContent))
                    throw new SendingException("不能发送空消息！");
                repo.SendMessage(message.MessageContent, message.Sender, message.SendTime, targets);
                ViewBag.ReturnMessage = "发送成功！";
            }
            catch (SendingException sendingEx)
            {
                ViewBag.ReturnMessage = "发送失败！" + sendingEx.Message;
            }
            catch (Exception ex)
            {
                ViewBag.ReturnMessage = "发送失败！出现未知异常，请和管理员联系！" + ex.Message;
            }
            finally
            {
                #region 恢复视图模型

                UserFriendList fl = new UserFriendList();
                fl.UserId = userId;
                fl.FriendList = repo.GetFriendList(userId);
                ViewBag.Friends = fl;
                #endregion
                GetRole();
            }
            ViewBag.IsPostback = "true";
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

        public ActionResult ReceiveMessage()
        {
            GetRole();
            return View();
        }

        public ActionResult ReceiveAsync()
        {
            string userId = GetUserId();
            var receivedMessage = repo.ReceiveMessage(userId);

            List<Models.Message> viewModel = new List<Models.Message>();
            foreach (var item in receivedMessage)
            {
                Models.Message m = new Models.Message()
                {
                    MessageContent = item.message_content,
                    Sender = repo.GetUser(item.sender).name,
                    SendTime = item.send_time
                };

                viewModel.Add(m);
            }

            return PartialView("_receive", viewModel);
        }
    }
}