using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkillProfiWebApplication.IdentitySkillProfi;
using SkillProfiWebApplication.DataContext;


namespace SkillProfiWebApplication
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
            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });

            services.AddMvc();

            services.AddDbContext<SkillProfiContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SkillProfiContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false; // Не требовать цифры
                options.Password.RequireLowercase = false; // Не требовать буквы в нижнем регистре
                options.Password.RequireNonAlphanumeric = false; // Не требовать специальные символы
                options.Password.RequireUppercase = false; // Не требовать буквы в верхнем регистре
                options.Password.RequiredLength = 0; // Минимальная длина пароля
                options.Password.RequiredUniqueChars = 0; // Минимальное количество уникальных символов
            }
                );
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

                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "admin", template: "admin", defaults: new { controller = "Admin", action = "Login" });
                routes.MapRoute(name: "index", template: "index", defaults: new { controller = "Admin", action = "Index" });
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
