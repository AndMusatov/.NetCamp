using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_TWITTER.WEB_UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();

            /*services.AddDefaultIdentity<User>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<UserContext>();*/

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/account/google-login");
                })
                .AddGoogle(options =>
                 {
                     IConfigurationSection googleAuthNSection =
                         Configuration.GetSection("Authentication:Google");

                     options.ClientId = googleAuthNSection["ClientId"];
                     options.ClientSecret = googleAuthNSection["ClientSecret"];
                 });
            /*.AddTwitter(options =>
            {
                options.
            });*/
            /*services.AddAuthentication(o =>
            {
                // This forces challenge results to be handled by Google OpenID Handler, so there's no
                // need to add an AccountController that emits challenges for Login.
                o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                // This forces forbid results to be handled by Google OpenID Handler, which checks if
                // extra scopes are required and does automatic incremental auth.
                o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                // Default scheme that will handle everything else.
                // Once a user is authenticated, the OAuth2 token info is stored in cookies.
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });*/



            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            );

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotNet_TWITTER", Version = "v1" });
            });
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserContext dataContext)
        {
            dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotNet_TWITTER");
                c.RoutePrefix = string.Empty;
            });
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
        }
    }
}
