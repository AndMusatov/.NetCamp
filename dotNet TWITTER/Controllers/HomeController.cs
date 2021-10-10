using Microsoft.AspNetCore.Mvc;
using dotNet_TWITTER.Applications.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("ShowPosts")]
        public ActionResult Index(int Id)
        {
            /*var init = new IPostDataBase();
            List<Post> posts = new List<Post>();
            for (int i = 0; ; i++)
            {
                posts.Add(IPostDataBase.Send(i));
                if (string.IsNullOrEmpty(posts[i].Filling))
                {
                    return Ok(posts);
                }
            }*/
            return Ok(IPostDataBase.Send(Id));
        }
    }
}
