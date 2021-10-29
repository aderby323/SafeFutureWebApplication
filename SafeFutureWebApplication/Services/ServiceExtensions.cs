using Microsoft.Extensions.DependencyInjection;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Services
{
    public static class ServiceExtensions
    {

       public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}
