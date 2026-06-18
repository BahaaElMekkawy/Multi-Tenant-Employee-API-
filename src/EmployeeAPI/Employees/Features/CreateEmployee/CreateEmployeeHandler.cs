using EmployeeAPI.Employees.Dtos;
using EmployeeAPI.Employees.Models;
using EmployeeAPI.Shared.ValueObjects;
using Shared.CQRS;
using System.Text.Json;

namespace EmployeeAPI.Employees.Features.AddEmployee
{
    public record CreateEmployeeCommand(EmployeeDto Employee)
        : ICommand<CreateEmployeeResult>;

    public record CreateEmployeeResult(Guid Id);
    public class CreateEmployeeHandler(EmployeesDbContext dbContext) : ICommandHandler<CreateEmployeeCommand, CreateEmployeeResult>
    {
        public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = CreateNewEmployee(command.Employee);
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateEmployeeResult(employee.Id);
        }

        private Employee CreateNewEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                TenantId = employeeDto.TenantId,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Department = employeeDto.Department,
                Status = Enum.Parse<EmployeeStatus>(employeeDto.Status),
                Salary = Money.Of(employeeDto.Salary.AmountMinor, employeeDto.Salary.CurrencyCode),
                CustomData = employeeDto.CustomData != null ? JsonDocument.Parse(JsonSerializer.Serialize(employeeDto.CustomData)) : null
            };
            return employee;
        }
    }
}
