using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP1_BookmarksManager.Controllers
{
    public class BookmarksController : Controller
    {
        // GET: Bookmarks
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.BookmarkView bookmarkView)
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Models.BookmarkView bookmarkView)
        {
            return View();
        }

        public ActionResult Details(int Id)
        {
            return View();
        }

        public ActionResult Delete(int Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Models.BookmarkView bookmarkView)
        {
            return View();
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