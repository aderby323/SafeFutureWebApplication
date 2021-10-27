using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Services
{
    public static class ServiceExtensions
    {

       public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}
