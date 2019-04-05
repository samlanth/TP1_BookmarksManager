using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP1_BookmarksManager.Models;

namespace TP1_BookmarksManager.Controllers
{
    public class UsersController : Controller
    {
        private Models.BookmarksDBEntities DB = new Models.BookmarksDBEntities();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: Users
        public ActionResult Subscribe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Subscribe(UserView userView)
        {
            if (ModelState.IsValid)
            {
                User userFound = DB.Users.Where(u => u.UserName == userView.UserName).FirstOrDefault();
                if (userFound != null)
                {
                    ModelState.AddModelError("UserName", "This username is already taken");
                    return View();
                }
              
                User user = Models.User.FromUserView(userView);
                DB.Add(user);
                return Redirect("../Home");
            }
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginView loginView)
        {
            if (ModelState.IsValid)
            {
                User userFound = DB.Users.Where(u => u.UserName == loginView.UserName).FirstOrDefault();
                if (userFound == null)
                {
                    ModelState.AddModelError("UserName", "This username does not exist");
                    return View();
                }
                else
                {
                    if (userFound.Password != loginView.Password)
                    {
                        ModelState.AddModelError("password", "Wrong password");
                        return View();
                    }
                }
                OnlineUsers.AddSessionUser(userFound);
            }
            else
                return View();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Profil()
        {
            return View();
            //return RedirectToAction("../Users/Subscribe", "User");
        }
        [HttpPost]
        public ActionResult Profil(UserView loginView)
        {
            User loggedUser = OnlineUsers.GetSessionUser();
            if (ModelState.IsValid)
            {
                User userFound = DB.Users.Where(u => u.UserName == loginView.UserName).FirstOrDefault();
                if (userFound != null)
                {
                    ModelState.AddModelError("UserName", "This username is already taken");
                    return View();
                }

                User user = Models.User.FromUserView(loginView);
                DB.Add(user);
                return Redirect("../Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            OnlineUsers.RemoveSessionUser();
            return RedirectToAction("../Users/Login", "User");

        }
    }
}