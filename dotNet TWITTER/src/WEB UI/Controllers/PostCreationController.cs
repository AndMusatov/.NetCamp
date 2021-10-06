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

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class PostCreationController : Controller
    {
        public ActionResult Index()
        {
            return View("PostCreation");
        }

        [HttpPost("PostCreation")]
        public ActionResult Create(PostCreationModel post)
        {
            var host = Program.CreateHostBuilder(Program.arg).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<PostContext>();
                    PostCreationModel.Create(context, post.PostFilling);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return View("PostCreation");
        }
    }
}
