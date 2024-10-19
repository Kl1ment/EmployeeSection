using EmployeeSection.Core.Models;
using EmployeeSection.DataAccess.Entities;

namespace EmployeeSection.DataAccess
{
    public static class MappingExtensions
    {
        public static EmployeeEntity MapToEntity(this Employee employee)
        {
            return new EmployeeEntity
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Profession = employee.Profession
            };
        }

        public static Employee MapToModel(this EmployeeEntity employeeEntity)
        {
            return Employee.Create(
                employeeEntity.Id,
                employeeEntity.FullName,
                employeeEntity.Profession);
        }
    }
}
