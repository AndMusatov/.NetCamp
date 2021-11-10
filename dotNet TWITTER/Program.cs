using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;

namespace dotNet_TWITTER.WEB_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
