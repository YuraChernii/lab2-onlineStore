using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tymchak_shop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Hosting;
using Tymchak_shop.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Tymchak_shop.Data.Repository;
using Tymchak_shop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
//
//
//
//To go to the page "For administration" use this username = 1234ddfSD and password = 1234ddfSD;
//
//
//
namespace Tymchak_shop
{
    public class Startup
    {
        private IConfiguration _config;
        private IConfigurationRoot _confString;
        public Startup(IWebHostEnvironment hostEnv, IConfiguration config) 
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
            _config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAllShoes, ShoesRepository>();
            services.AddTransient<IShoesCategory, CategoryRepository>();
            services.AddTransient<IAllOrders, OrdersRepository>();
            //services.AddMvc();(_confString.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
            var mailKitOption = _config.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config => config.UseMailKit(mailKitOption));
            //services.AddControllersWithViews();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route =>
            {
                route.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                route.MapRoute(name: "categoryFilter", template: "Shoes/{action}/{category?}", defaults: new { Controller = "Shoes", action = "List" });
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
                
        }
    }
}
