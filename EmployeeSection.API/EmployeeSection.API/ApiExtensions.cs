using EmployeeSection.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSection.API
{
    public static class ApiExtensions
    {
        public static void ApplyMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<EmployeeSectionDbContext>();

                if (context.Database.GetMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
