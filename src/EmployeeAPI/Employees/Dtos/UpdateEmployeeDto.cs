namespace EmployeeAPI.Employees.Dtos
{
    public record UpdateEmployeeDto(
            string FirstName,
            string LastName,
            string Email,
            string Department,
            string Status,
            Dictionary<string, object>? CustomData,
            MoneyDto Salary);
    
}
