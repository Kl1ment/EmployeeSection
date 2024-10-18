using EmployeeSection.DataAccess.Configurations;
using EmployeeSection.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSection.DataAccess
{
    public class EmployeeSectionDbContext : DbContext
    {
        public EmployeeSectionDbContext(DbContextOptions<EmployeeSectionDbContext> options)
            :base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
