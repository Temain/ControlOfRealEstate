using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ControlOfRealEstate
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseVkontakteAuthentication(options =>
            {
                options.AuthenticationScheme = "vk";
                options.ClientId = "5904669";
                options.ClientSecret = "ST6pUuuS4g6hPsmM3UzP";
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "google",
                ClientId = "119706625951-nk8nho1ipvq167afmsiut86j2g2mui1r.apps.googleusercontent.com",
                ClientSecret = "4RMMVRKzxZ8TFGIT5NIcALuu"
            });

            //app.UseGoogleAuthentication(new GoogleOptions
            //{
            //    AuthenticationScheme = "google",
            //    ClientId = "119706625951-nk8nho1ipvq167afmsiut86j2g2mui1r.apps.googleusercontent.com",
            //    ClientSecret = "4RMMVRKzxZ8TFGIT5NIcALuu"
            //});

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "moderation",
                    template: "moderation/editobject/{illegalObjectId?}",
                    defaults: new { controller = "Moderation", action = "EditObject" });

                routes.MapRoute(
                    name: "profile",
                    template: "profile/{userId?}",
                    defaults: new { controller = "Profile", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "forum",
                    template: "forum/{id?}",
                    defaults: new { controller = "Forum", action = "Index" });
            });
        }
    }
}
