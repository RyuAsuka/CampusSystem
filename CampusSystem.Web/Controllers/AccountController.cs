using CampusSystem.Data;
using CampusSystem.Data.Models;
using CampusSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CampusSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(user.UserId == null && user.Password == null)
                {
                    ViewBag.ErrorMessage = "请输入用户名和密码";
                    return View(user);
                }
                if (repo.VerifyUser(user.UserId, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserId, user.AutoLogin);
                    //logger.InfoFormat("用户{0}登录成功",loginModel.UserId);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                                            && returnUrl.StartsWith("/")
                                            && !returnUrl.StartsWith("//") &&
                                            !returnUrl.StartsWith("/\\"))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                //logger.ErrorFormat("用户{0}试图登录失败",loginModel.UserId);
                ModelState.AddModelError("", "登录失败，用户名或密码错误");
                ViewBag.ErrorMessage = "登录失败，用户名或密码错误";
            }
            return View(user);
        }

        //
        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}