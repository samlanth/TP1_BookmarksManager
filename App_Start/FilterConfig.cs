﻿using System.Web;
using System.Web.Mvc;

namespace TP1_BookmarksManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
