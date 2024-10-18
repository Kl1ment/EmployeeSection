namespace EmployeeSection.Core.Models
{
    public class Employee
    {
        public Guid Id { get; }
        public string FullName { get; }
        public string Profession { get; }

        private Employee(Guid id, string fullName, string profession)
        {
            Id = id;
            FullName = fullName;
            Profession = profession;
        }

        public static Employee Create(Guid id, string fullName, string profession)
        {
            return new Employee(id, fullName, profession);
        }
    }
}
