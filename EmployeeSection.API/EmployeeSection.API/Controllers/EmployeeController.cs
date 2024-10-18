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
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById([FromQuery] Guid id)
        {
            var employeeRequest = await _employeeService.GetEmployeeByIdAsync(id);

            return employeeResult(employeeRequest);
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByFullName(string fullName)
        {
            var employeeRequest = await _employeeService.GetEmployeeByFullName(fullName);

            return employeeResult(employeeRequest);
        }


        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponse>>> GetEmployeeList([FromQuery] int page)
        {
            var employees = await _employeeService.GetEmployeesAsync(page);

            return Ok(employees.Select(e => new EmployeeResponse(
                e.Id,
                e.FullName,
                e.Profession)).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEmployee(EmployeeCreate employee)
        {
            var creationResult = await _employeeService.CreateEmployeeAsync(
                employee.FullName,
                employee.Profession);

            if (creationResult.IsFailure) 
                return BadRequest(creationResult.Error);

            return RedirectToAction(nameof(GetEmployeeById), new { id = creationResult.Value });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee([FromQuery] Guid id, EmployeeCreate employee)
        {
            var updateResult = await _employeeService.UpdateEmployeeAsync(
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
            var deletionResult = await _employeeService.DeleteEmployeeAsync(id);

            if (deletionResult.IsFailure)
                return BadRequest(deletionResult.Error);

            return Ok(deletionResult.Value);
        }

        private ActionResult<EmployeeResponse> employeeResult(CSharpFunctionalExtensions.Result<Employee> employeeRequest)
        {
            if (employeeRequest.IsFailure)
                return NotFound(employeeRequest.Error);

            return Ok(new EmployeeResponse(
                employeeRequest.Value.Id,
                employeeRequest.Value.FullName,
                employeeRequest.Value.Profession));
        }
    }
}
