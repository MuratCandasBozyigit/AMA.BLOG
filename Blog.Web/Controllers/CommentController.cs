﻿using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
