using Microsoft.Extensions.DependencyInjection;
using SafeFutureWebApplication.Services.Interfaces;

namespace SafeFutureWebApplication.Services
{
    public static class ServiceExtensions
    {
        public const int DEFAULT_PAGE_SIZE = 5;
        public const int MAX_SEARCH_TERMS = 3;

        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
