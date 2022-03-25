using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HomeFinder.Models;
using HomeFinder.Repository;

namespace HomeFinder
{
    public class Startup
    {
        //Test
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HomeFinderContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HomeFinderContext")));
            services.AddRazorPages();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HomeFinderContext>();
            services.AddControllersWithViews();

            services.AddScoped<IItemRepository, ItemRepository>();

            services.AddAuthentication().AddGoogle(options =>
            {
                //IConfigurationSection googleAuthNSection =
                //    Configuration.GetSection("Authentication:Google");

                options.ClientId = "647277621048-aba8vqvo395blm6eq1llq8km3hocied2.apps.googleusercontent.com";
                options.ClientSecret = "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";
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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
                endpoints.MapRazorPages();
            });
        }
    }
}
