using CSharpFunctionalExtensions;
using EmployeeSection.Core.Models;

namespace EmployeeSection.Application
{
    public interface IEmployeeService
    {
        Task<Result<Guid>> CreateEmployeeAsync(string fullName, string profession);
        Task<Result<Guid>> DeleteEmployeeAsync(Guid id);
        Task<Result<Employee>> GetEmployeeByFullName(string fullName);
        Task<Result<Employee>> GetEmployeeByIdAsync(Guid id);
        Task<List<Employee>> GetEmployeesAsync(int page);
        Task<Result<Guid>> UpdateEmployeeAsync(Guid id, string fullName, string profession);
    }
}