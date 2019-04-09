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
            User loggedUser = OnlineUsers.GetSessionUser();
           
            UserView model = new UserView();
            model.FirstName = loggedUser.FirstName;
            model.BirthDate = loggedUser.BirthDate;
            model.Email = loggedUser.Email;
            model.LastName = loggedUser.LastName;
            model.Password = loggedUser.Password;
            model.UserName = loggedUser.UserName;
            model.ConfirmPassword = loggedUser.Password;
            model.Sex = (SexType)loggedUser.Sex;

            return View(model);
        }
        [HttpPost]
        public ActionResult Profil(UserView profilView)
        {
            if (ModelState.IsValid)
            {
                User loggedUser = OnlineUsers.GetSessionUser();
                loggedUser.FirstName = profilView.FirstName;
                loggedUser.LastName = profilView.LastName;
                loggedUser.BirthDate = profilView.BirthDate;
                loggedUser.Password = profilView.Password;
                loggedUser.Password = profilView.ConfirmPassword;
                loggedUser.Sex = (int)profilView.Sex;
                loggedUser.Email = profilView.Email;
                loggedUser.BirthDate = profilView.BirthDate;
                DB.Update(loggedUser);
                return RedirectToAction("Index", "Home"); // or whatever
            }
            return View(profilView);
        }
        public ActionResult Logout()
        {
            OnlineUsers.RemoveSessionUser();
            return RedirectToAction("../Users/Login", "User");
        }
    }
}