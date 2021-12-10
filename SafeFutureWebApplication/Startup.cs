using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services;
using SafeFutureWebApplication.Services.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SafeFutureWebApplication
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
            services.AddControllersWithViews();

            services.AddTransient<IAuthService, AuthService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppDb"));
            });

            services.AddProjectServices();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.Cookie.Name = "SFFISLogin";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Admin", "Dev" }));

                options.AddPolicy("Staff",
                    policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Staff", "Dev" }));
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = "SFFIS.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(5);
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
