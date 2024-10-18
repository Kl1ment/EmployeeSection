using EmployeeSection.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSection.Application
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
