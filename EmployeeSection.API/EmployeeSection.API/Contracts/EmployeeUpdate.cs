namespace EmployeeSection.API.Contracts
{
    public record EmployeeUpdate(
        Guid Id,
        string FullName,
        string Profession);
}
