using dotNet_TWITTER.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Controllers
{
    public class HomeController : Controller
    {
        PostContext db;
        public HomeController(PostContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Posts.ToList());
        }
    }
}
