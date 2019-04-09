using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP1_BookmarksManager.Models;

namespace TP1_BookmarksManager.Controllers
{
    public class BookmarksController : Controller
    {
        private BookmarksDBEntities DB = new BookmarksDBEntities();
        private Bookmark Bookmarks = new Bookmark();
        // GET: Bookmarks
        public ActionResult Index()
        {
            return View(DB.Bookmarks.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookmarkView bookmarkView) // id, ownerid
        {
            User loggedUser = OnlineUsers.GetSessionUser();
            if (ModelState.IsValid)
            {
                Bookmark bookmarkFound = DB.Bookmarks.Where(u => u.Name == bookmarkView.Name).FirstOrDefault();
                if (bookmarkFound != null)
                {
                    ModelState.AddModelError("Bookmark", "This Bookmark is already taken");
                    return View();
                }
                Bookmark bookmark = Models.Bookmark.FromBookmarkView(bookmarkView);
                bookmark.UserId = loggedUser.Id;
                DB.Add(bookmark);
                return Redirect("../Home");
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            Bookmark bookmarkFound = DB.Bookmarks.Where(u => u.Id == Id).FirstOrDefault();
            BookmarkView bookmark = new BookmarkView();
            bookmark.OwnerId = Id;
            bookmark.Name = bookmarkFound.Name;
            bookmark.Url = bookmarkFound.Url;
            bookmark.Shared = bookmarkFound.Shared;
            bookmark.newCategory = bookmarkFound.Category.Name;
            bookmark.CategoryId = bookmarkFound.Category.Id;
            
            return View(bookmark);
        }

        [HttpPost]
        public ActionResult Edit(BookmarkView bookmarkView)
        {
            User loggedUser = OnlineUsers.GetSessionUser();
            if (ModelState.IsValid)
            {
                Bookmark bookmark = new Bookmark();
                bookmark.Id = bookmarkView.Id;
                bookmark.Name = bookmarkView.Name;
                bookmark.Url = bookmarkView.Url;
                bookmark.Shared = bookmarkView.Shared;
                bookmark.Category = new Category();
                bookmark.Category.Id = (int)bookmarkView.CategoryId;
                bookmark.Category.Name = bookmarkView.newCategory;
                bookmark.UserId = loggedUser.Id;
                bookmark.CategoryId = bookmarkView.CategoryId;
                DB.Update(bookmark);
                return RedirectToAction("Index", "Home");
            }
            return View(bookmarkView);
        }

        public ActionResult Details(int Id)
        {
            Bookmark bookmarkFound = DB.Bookmarks.Where(u => u.Id == Id).FirstOrDefault();
            BookmarkItemView bookmark = new BookmarkItemView();
            bookmark.Name = bookmarkFound.Name;
            bookmark.Url = bookmarkFound.Url;
            bookmark.OwnerShip = bookmarkFound.User.GetFullName();
            return View(bookmark);
        }

        public ActionResult Delete(int Id)
        {
            Bookmark bookmarkFound = DB.Bookmarks.Where(u => u.Id == Id).FirstOrDefault();
            BookmarkView bookmark = new BookmarkView();
            bookmark.OwnerId = Id;
            bookmark.Name = bookmarkFound.Name;
            bookmark.Url = bookmarkFound.Url;
            bookmark.Shared = bookmarkFound.Shared;
            bookmark.newCategory = bookmarkFound.Category.Name;
            bookmark.CategoryId = bookmarkFound.Category.Id;
            return View(bookmark);
        }

        [HttpPost]
        public ActionResult Delete(BookmarkView bookmarkView)
        {
            User loggedUser = OnlineUsers.GetSessionUser();

            Bookmark bookmark = new Bookmark();
            bookmark.Id = bookmarkView.Id;
            DB.Delete(bookmark);
            return RedirectToAction("Index", "Home");
        }

        #region Sort and filters
        private void InitSessionSortAndFilter()
        {
            if (Session["BookmarkSortBy"] == null)
            {
                Session["BookmarkSortBy"] = "Name";
                Session["BookmarkSortAscendant"] = true;
            }

            if (Session["BookmarkFilterByOwnership"] == null)
            {
                Session["BookmarkFilterByOwnership"] = "";
            }

            if (Session["BookmarkFilterByCategory"] == null)
            {
                Session["BookmarkFilterByCategory"] = "All";
            }
        }

        public ActionResult Sort(string by)
        {
            if (by == (string)Session["BookmarkSortBy"])
                Session["BookmarkSortAscendant"] = !(bool)Session["BookmarkSortAscendant"];
            else
                Session["BookmarkSortAscendant"] = true;

            Session["BookmarkSortBy"] = by;
            return RedirectToAction("Index");
        }

        public ActionResult FilterOwnership(string Ownership)
        {
            Session["BookmarkFilterByOwnership"] = (Ownership == "All" ? "" : Ownership);
            return RedirectToAction("Index");
        }

        public ActionResult FilterCategory(string Category)
        {
            Session["BookmarkFilterByCategory"] = Category;
            return RedirectToAction("Index");
        }
        #endregion
    }
}