using EmployeeAPI.Employees.Dtos;
using EmployeeAPI.Employees.Models;
using EmployeeAPI.Shared.ValueObjects;
using EmployeeAPI.Tenancy;
using Shared.CQRS;
using System.Text.Json;

namespace EmployeeAPI.Employees.Features.AddEmployee
{
    public record CreateEmployeeCommand(CreateEmployeeDto Employee)
        : ICommand<CreateEmployeeResult>;

    public record CreateEmployeeResult(Guid Id);

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee).NotNull();
            RuleFor(x => x.Employee.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Employee.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Employee.Email).NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.Employee.Department).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Employee.Status).NotEmpty().Must(status => Enum.TryParse<EmployeeStatus>(status, out _)).WithMessage("Invalid employee status.");
            RuleFor(x => x.Employee.Salary).NotNull();
            RuleFor(x => x.Employee.Salary.AmountMinor).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Employee.Salary.CurrencyCode).NotEmpty().Length(3);
        }
    }
    public class CreateEmployeeCommandHandler(EmployeesDbContext dbContext , ITenantService tenantService) : ICommandHandler<CreateEmployeeCommand, CreateEmployeeResult>
    {
        private readonly ITenantService tenantService = tenantService;

        public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = CreateNewEmployee(command.Employee);
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateEmployeeResult(employee.Id);
        }

        private Employee CreateNewEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                TenantId = tenantService.TenantId,
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
