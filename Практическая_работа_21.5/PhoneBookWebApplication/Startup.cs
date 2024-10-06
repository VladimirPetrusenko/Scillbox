using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PhoneBookWebApplication.Interfaces;
using Microsoft.AspNetCore.Identity;
using PhoneBookWebApplication.AuthIdentityPhoneBookWebApplication;
using PhoneBookWebApplication.DataContext;

namespace PhoneBookWebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });

            services.AddMvc();

            services.AddDbContext<PhoneBookWebApplicationContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IPhoneBookData, PhoneBookData>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PhoneBookWebApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
            }
                );

            services.ConfigureApplicationCookie(options =>
            {
               
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                
            }
                );

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }
            
            app.UseStaticFiles();

            
            app.UseAuthentication();
           

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=index}/{id?}");
            });
        }
    }
}
