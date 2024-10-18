namespace EmployeeSection.DataAccess.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
    }
}
