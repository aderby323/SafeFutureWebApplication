using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Services;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace SafeFutureWebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment host)
        {
            Configuration = configuration;
            Host = host;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Host { get; }

        public void ConfigureServices(IServiceCollection services)
        {              
            services.AddProjectServices();

            if (Host.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
                var connectionString = "U2VydmVyPXRjcDpzZmZzZXJ2ZXIxLmRhdGFiYXNlLndpbmRvd3MubmV0LDE0MzM7SW5pdGlhbCBDYXRhbG9nPVNGRkRhdGFiYXNlO1BlcnNpc3QgU2VjdXJpdHkgSW5mbz1GYWxzZTtVc2VyIElEPXNlcnZlcmFkbWluO1Bhc3N3b3JkPUxUYyxANjV6O011bHRpcGxlQWN0aXZlUmVzdWx0U2V0cz1GYWxzZTtFbmNyeXB0PVRydWU7VHJ1c3RTZXJ2ZXJDZXJ0aWZpY2F0ZT1GYWxzZTtDb25uZWN0aW9uIFRpbWVvdXQ9MzA7";
 
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException("Connection string is empty");
                }

                var bytes = Convert.FromBase64String(connectionString);
                connectionString = System.Text.Encoding.UTF8.GetString(bytes);
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            else
            {
                services.AddControllersWithViews();
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(Environment.GetEnvironmentVariable("SQLCONNSTR_AppDb"));
                });
            }          

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
                app.UseExceptionHandler("/error");
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
