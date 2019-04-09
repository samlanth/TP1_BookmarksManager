using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP1_BookmarksManager.Models;

namespace TP1_BookmarksManager.Controllers
{
    [AdminAccess]
    public class AdministratorController : Controller
    {
        private BookmarksDBEntities DB = new BookmarksDBEntities();
        
        // GET: Administrator
        public ActionResult Index()
        {
            //List<User> users = new List<User>();
            //User u = new User();
            //users.Add(u);
            //return View(users);
            return View(DB.Users.ToList());
        }
        public ActionResult Delete(int Id)
        {
            int i = 0;
            User userFound = DB.Users.Where(u => u.Id == Id).FirstOrDefault();
            foreach (var items in userFound.Bookmarks)
            {
                i++;
            }
            
            //Bookmark bookmarkFound = DB.Bookmarks.Where(u => u.UserId == Id).FirstOrDefault();
            //BookmarkView bookmark = new BookmarkView();
            //bookmark.OwnerId = Id;
            //bookmark.Name = bookmarkFound.Name;
            //bookmark.Url = bookmarkFound.Url;
            return View(userFound);
        }

        [HttpPost]
        public ActionResult Delete(UserView userView)
        {
            return View();
            //User loggedUser = OnlineUsers.GetSessionUser();

            //Bookmark bookmark = new Bookmark();
            //bookmark.Id = bookmarkView.Id;
            //DB.Delete(bookmark);
            //return RedirectToAction("Index", "Home");
        }
    }
}