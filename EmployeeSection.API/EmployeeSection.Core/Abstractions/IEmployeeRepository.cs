using CSharpFunctionalExtensions;
using EmployeeSection.Core.Models;

namespace EmployeeSection.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Guid> AddAsync(Employee employee);
        Task<Result<Guid>> DeleteAsync(Guid id);
        Task<Result<Employee>> GetByFullNameAsync(string fullName);
        Task<Result<Employee>> GetByIdAsync(Guid id);
        Task<List<Employee>> GetListAsync(int page);
        Task<Result<Guid>> UpdateAsync(Employee employee);
    }
}