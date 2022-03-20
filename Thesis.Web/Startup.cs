using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Thesis.Web.Data;
using Thesis.Web.Models;
using Thesis.Web.Services;

namespace Thesis.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Configuring the Identity Model to use customized User and Role Profile

            services.AddIdentity<MyIdentityUser, MyIdentityRole>(options =>
            {
                //Sign-In Policy
                options.SignIn.RequireConfirmedAccount = true;

                //Password Policy
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                //User Policy
                options.User.RequireUniqueEmail = true;


                //Phone Number Policy


            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            /*   
               services.AddDefaultIdentity<MyIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddEntityFrameworkStores<ApplicationDbContext>();*/


            //Configure the Identity Application Cookie Policy
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
            });

            //Registering the customized Email Services for dependency injection
            //Only one copy of the services should be instantiated
            services
                .AddSingleton<IEmailSender, MyEmailSender>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
             ILogger<Startup> logger,
            UserManager<MyIdentityUser> userManager,
            RoleManager<MyIdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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

                endpoints.MapRazorPages();

                // Register the Route for Areas
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // Register the Default Route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            ApplicationDbContextSeed.SeedIdentityRolesAsync(roleManager).Wait();
            ApplicationDbContextSeed.SeedIdentityUserAsync(userManager).Wait();
        }
    }
}
