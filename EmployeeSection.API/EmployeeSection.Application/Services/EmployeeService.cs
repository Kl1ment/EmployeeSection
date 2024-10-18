using CSharpFunctionalExtensions;
using EmployeeSection.Core.Models;
using EmployeeSection.DataAccess.Repositories;

namespace EmployeeSection.Application.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<Result<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<Result<Employee>> GetEmployeeByFullName(string fullName)
        {
            return await _employeeRepository.GetByFullNameAsync(fullName);
        }

        public async Task<List<Employee>> GetEmployeesAsync(int page)
        {
            if (page <= 0)
                return new List<Employee>();

            return await _employeeRepository.GetListAsync(page);
        }

        public async Task<Result<Guid>> CreateEmployeeAsync(string fullName, string profession)
        {
            var checkingEmployeeExistence = await _employeeRepository.GetByFullNameAsync(fullName);

            if (checkingEmployeeExistence.IsSuccess)
                return Result.Failure<Guid>("Such an employee already exists");

            var employee = Employee.Create(
                Guid.NewGuid(),
                fullName,
                profession);

            return await _employeeRepository.AddAsync(employee);
        }

        public async Task<Result<Guid>> UpdateEmployeeAsync(Guid id, string fullName, string profession)
        {
            var checkingEmployeeExistenceById = await _employeeRepository.GetByIdAsync(id);

            if (checkingEmployeeExistenceById.IsFailure)
                return Result.Failure<Guid>(checkingEmployeeExistenceById.Error);

            var checkingEmployeeExistence = await _employeeRepository.GetByFullNameAsync(fullName);

            if (checkingEmployeeExistence.IsSuccess && checkingEmployeeExistence.Value.Id != id)
                return Result.Failure<Guid>("Such an employee already exists");

            var employee = Employee.Create(
                id,
                fullName,
                profession);

            return await _employeeRepository.UpdateAsync(employee);
        }

        public async Task<Result<Guid>> DeleteEmployeeAsync(Guid id)
        {
            var checkingEmployeeExistence = await _employeeRepository.GetByIdAsync(id);

            if (checkingEmployeeExistence.IsFailure)
                return Result.Failure<Guid>(checkingEmployeeExistence.Error);

            return await _employeeRepository.DeleteAsync(id);
        }
    }
}
