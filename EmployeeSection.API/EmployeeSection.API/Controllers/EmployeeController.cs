using CSharpFunctionalExtensions;
using EmployeeSection.API.Contracts;
using EmployeeSection.Application;
using EmployeeSection.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSection.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById([FromQuery] Guid id)
        {
            var employeeRequest = await employeeService.GetEmployeeByIdAsync(id);

            return EmployeeResult(employeeRequest);
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByFullName(string fullName)
        {
            var employeeRequest = await employeeService.GetEmployeeByFullName(fullName);

            return EmployeeResult(employeeRequest);
        }


        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponse>>> GetEmployeeList([FromQuery] int page)
        {
            var employees = await employeeService.GetEmployeesAsync(page);

            return Ok(employees.Select(e => e.MapToResponse()));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEmployee(EmployeeCreate employee)
        {
            var creationResult = await employeeService.CreateEmployeeAsync(
                employee.FullName,
                employee.Profession);

            if (creationResult.IsFailure) 
                return BadRequest(creationResult.Error);

            return RedirectToAction(nameof(GetEmployeeById), new { id = creationResult.Value });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee([FromQuery] Guid id, EmployeeCreate employee)
        {
            var updateResult = await employeeService.UpdateEmployeeAsync(
                id,
                employee.FullName,
                employee.Profession);

            if (updateResult.IsFailure)
                return BadRequest(updateResult.Error);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee([FromQuery] Guid id)
        {
            var deletionResult = await employeeService.DeleteEmployeeAsync(id);

            if (deletionResult.IsFailure)
                return BadRequest(deletionResult.Error);

            return Ok(deletionResult.Value);
        }

        private ActionResult<EmployeeResponse> EmployeeResult(Result<Employee> employeeRequest)
        {
            if (employeeRequest.IsFailure)
                return NotFound(employeeRequest.Error);

            return Ok(employeeRequest.Value.MapToResponse());
        }
    }
}
