using EmployeeSection.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSection.DataAccess
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EmployeeSectionDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(EmployeeSectionDbContext)));
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }

    }
}
