namespace EmployeeAPI.Employees.Dtos
{
    public record EmployeeDto(Guid Id,
        Guid TenantId,
        string FirstName,
        string LastName,
        string Email,
        string Department,
        string Status,
        Dictionary<string, object>? CustomData,
        MoneyDto Salary);
   
}
