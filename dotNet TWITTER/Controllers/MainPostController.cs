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
    public class MainPostController : Controller
    {

        [HttpPost("PostCreation")]
        public ActionResult CreatePost(string filling)
        {
            return Ok(IPostDataBase.AddPost(filling));
        }

        [HttpGet("ShowPost")]
        public ActionResult Index(int id)
        {
            //var init = new IPostDataBase();
            return Ok(IPostDataBase.SendPost(id));
        }
    }
}

