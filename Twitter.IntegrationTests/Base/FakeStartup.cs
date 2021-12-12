using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.WEB_UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
    public class FakeStartup //: Startup
    {
        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var dbContextDesriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<UserContext>));

            services.Remove(dbContextDesriptor);

            services.AddDbContext<UserContext>(options =>
            {
                options.UseInMemoryDatabase("twitter_test_db");
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/account/google-login");
                });
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = "Application";
                o.DefaultSignInScheme = "External";
            })
            .AddCookie("Application")
            .AddCookie("External");



            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            );

            services.AddControllersWithViews();
            services.AddMvc();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddTransient<ISubRepository, SubRepository>();
            services.AddScoped<ISubRepository, SubRepository>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "GoogleAuthController",
                    pattern: "{controller=GoogleAuthController}/{action=GoogleLogin}");
            });

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<UserContext>();
                if (dbContext == null)
                {
                    throw new NullReferenceException("Cannot get instance of dbContext");
                }

                /*if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("live.db"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }*/

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                dbContext.Users.Add(new User { Id = "1", UserName = "UserTest1", Email = "test1@gmail.com" });
                dbContext.Users.Add(new User { Id = "2", UserName = "UserTest2", Email = "test2@gmail.com" });
                dbContext.SaveChanges();
            }
        }
    }
}
