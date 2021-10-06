using Microsoft.AspNetCore.Mvc;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class HomeController : Controller
    {
        PostContext db;
        public HomeController(PostContext context)
        {
            db = context;
        }
        [HttpPost("Show Posts")]
        public IActionResult Index()
        {
            return View(db.Posts.ToList());
        }
    }
}
