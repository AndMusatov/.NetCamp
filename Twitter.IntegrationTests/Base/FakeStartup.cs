using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.WEB_UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.IntegrationTests.Base
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        [Obsolete]
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<UserContext>();

                if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("DefaultConnection"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }

                // Initialize database
            }
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseInMemoryDatabase("customerDb_test"));
        }
    }
}
