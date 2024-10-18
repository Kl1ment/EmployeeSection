namespace EmployeeSection.API.Contracts
{
    public record EmployeeResponse(
        Guid Id,
        string FullName,
        string Profession);
}
