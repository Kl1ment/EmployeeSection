using CSharpFunctionalExtensions;
using EmployeeSection.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSection.DataAccess.Repositories
{
    public class EmployeeRepository(EmployeeSectionDbContext context) : IEmployeeRepository
    {
        public const int PageSize = 10;

        private readonly EmployeeSectionDbContext _context = context;

        public async Task<Result<Employee>> GetByIdAsync(Guid id)
        {
            var employeeEntity = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employeeEntity == null)
                return Result.Failure<Employee>("The employee was not found");

            var employee = employeeEntity.MapToModel();

            return employee;
        }

        public async Task<Result<Employee>> GetByFullNameAsync(string fullName)
        {
            var employeeEntity = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.FullName == fullName);

            if (employeeEntity == null)
                return Result.Failure<Employee>("The employee was not found");

            var employee = employeeEntity.MapToModel();

            return employee;
        }

        public async Task<List<Employee>> GetListAsync(int page)
        {
            var employeeEntities = await _context.Employees
                .AsNoTracking()
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var employees = employeeEntities.Select(e => 
                e.MapToModel()).ToList();

            return employees;
        }

        public async Task<Guid> AddAsync(Employee employee)
        {
            var employeeEntity = employee.MapToEntity();

            await _context.AddAsync(employeeEntity);
            await _context.SaveChangesAsync();

            return employeeEntity.Id;
        }

        public async Task<Guid> UpdateAsync(Employee employee)
        {
            await _context.Employees
                .Where(e => e.Id == employee.Id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(p => p.FullName, employee.FullName)
                    .SetProperty(p => p.Profession, employee.Profession));

            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.Employees
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            return id;
        }
    }
}
