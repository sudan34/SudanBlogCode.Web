﻿using Microsoft.AspNetCore.Mvc;

namespace SudanBlogCode.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
