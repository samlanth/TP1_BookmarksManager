using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP1_BookmarksManager.Models;

namespace TP1_BookmarksManager.Controllers
{
    public class UserAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (OnlineUsers.GetSessionUser() != null)
                return true;
            else
                httpContext.Response.Redirect("/Users/Login");
            return base.AuthorizeCore(httpContext);
        }
    }
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User sessionUser = OnlineUsers.GetSessionUser();
            if (sessionUser != null)
                if (sessionUser.Admin)
                    return true;
                else
                    httpContext.Response.Redirect("/Users/Login");
            return base.AuthorizeCore(httpContext);
        }
    }
}