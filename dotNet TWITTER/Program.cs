using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.EntityFrameworkCore;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.WEB_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var initDataBase = new IPostDataBase();
            var initUsers = new IUserDataBase();
            var initLogin = new LoginStatusDTO();
            var initMainPostActions = new MainPostsActions();
            var initCommentsActions = new CommentsActions();
            var initLikes = new LikesActions();
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
