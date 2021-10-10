using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.WEB_UI;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class PostCreationController : Controller
    {
        public ActionResult Index()
        {
            return View("PostCreation");
        }

        [HttpPost("PostCreation")]
        public ActionResult Create(string filling)
        {
            var service = new IPostDataBase();
            service.Create(filling);
            return Ok();
        }
    }
}

