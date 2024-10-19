using CSharpFunctionalExtensions;
using EmployeeSection.Core.Models;
using EmployeeSection.DataAccess.Repositories;

namespace EmployeeSection.Application.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        public async Task<Result<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            return await employeeRepository.GetByIdAsync(id);
        }

        public async Task<Result<Employee>> GetEmployeeByFullName(string fullName)
        {
            return await employeeRepository.GetByFullNameAsync(fullName);
        }

        public async Task<List<Employee>> GetEmployeesAsync(int page)
        {
            if (page <= 0)
                return new List<Employee>();

            return await employeeRepository.GetListAsync(page);
        }

        public async Task<Result<Guid>> CreateEmployeeAsync(string fullName, string profession)
        {
            var checkingEmployeeExistence = await employeeRepository.GetByFullNameAsync(fullName);

            if (checkingEmployeeExistence.IsSuccess)
                return Result.Failure<Guid>("Such an employee already exists");

            var employee = Employee.Create(
                Guid.NewGuid(),
                fullName,
                profession);

            return await employeeRepository.AddAsync(employee);
        }

        public async Task<Result<Guid>> UpdateEmployeeAsync(Guid id, string fullName, string profession)
        {
            var checkingEmployeeExistenceById = await employeeRepository.GetByIdAsync(id);

            if (checkingEmployeeExistenceById.IsFailure)
                return Result.Failure<Guid>(checkingEmployeeExistenceById.Error);

            var checkingEmployeeExistence = await employeeRepository.GetByFullNameAsync(fullName);

            if (checkingEmployeeExistence.IsSuccess && checkingEmployeeExistence.Value.Id != id)
                return Result.Failure<Guid>("Such an employee already exists");

            var employee = Employee.Create(
                id,
                fullName,
                profession);

            return await employeeRepository.UpdateAsync(employee);
        }

        public async Task<Result<Guid>> DeleteEmployeeAsync(Guid id)
        {
            var checkingEmployeeExistence = await employeeRepository.GetByIdAsync(id);

            if (checkingEmployeeExistence.IsFailure)
                return Result.Failure<Guid>(checkingEmployeeExistence.Error);

            return await employeeRepository.DeleteAsync(id);
        }
    }
}
