using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampusSystem.Data;
using CampusSystem.Data.Models;
using CampusSystem.Web.Models;

namespace CampusSystem.Web.Models
{
    public class CampusAuthorizeAttribute : AuthorizeAttribute
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());
        // 只需重载此方法，模拟自定义的角色授权机制  
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string currentRole = GetRole(httpContext.User.Identity.Name);
            if(currentRole == null) return base.AuthorizeCore(httpContext);
            if (Roles.Contains(currentRole))
                return true;
            return base.AuthorizeCore(httpContext);
        }

        // 返回用户对应的角色， 在实际中， 可以从SQL数据库中读取用户的角色信息  
        private string GetRole(string userId)
        {
            var user = repo.GetUser(userId);
            if(user != null)
                return user.role;
            return null;
        }  
    }
}