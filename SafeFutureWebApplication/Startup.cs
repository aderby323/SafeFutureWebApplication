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
using SafeFutureWebApplication.Models.Options;

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
                var connectionString = Environment.GetEnvironmentVariable("SFFDb");
 
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

            AuthOptions authOptions = new AuthOptions();
            Configuration.GetSection(AuthOptions.Key).Bind(authOptions);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim(ClaimTypes.Role, authOptions.AdminGroup));

                options.AddPolicy("Staff",
                    policy => policy.RequireClaim(ClaimTypes.Role, authOptions.StaffGroup));

                options.AddPolicy("Dev",
                    policy => policy.RequireClaim(ClaimTypes.Role, authOptions.DevGroup));
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = "SFFIS.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
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
