using EmployeeAPI.Employees.Models;
using EmployeeAPI.Shared.ValueObjects;
using System.Text.Json;

namespace EmployeeAPI.Employees.Features.UpdateEmployee
{
    public record UpdateEmployeeCommand(Guid Id, UpdateEmployeeDto Employee)
    : ICommand<UpdateEmployeeResult>;
    public record UpdateEmployeeResult(bool IsSuccess);

    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Employee ID is required.");
            RuleFor(x => x.Employee).NotNull();
            RuleFor(x => x.Employee.FirstName).NotEmpty().MaximumLength(100).WithMessage("First name is required.");
            RuleFor(x => x.Employee.LastName).NotEmpty().MaximumLength(100).WithMessage("Last name is required.");
            RuleFor(x => x.Employee.Email).NotEmpty().EmailAddress().MaximumLength(255).WithMessage("Invalid email address.");
            RuleFor(x => x.Employee.Department).NotEmpty().MaximumLength(100).WithMessage("Department is required.");
            RuleFor(x => x.Employee.Status)
                .NotEmpty()
                .Must(status => Enum.TryParse<EmployeeStatus>(status, true, out _))
                .WithMessage("Invalid employee status.");
            RuleFor(x => x.Employee.Salary).NotNull();
            RuleFor(x => x.Employee.Salary.AmountMinor).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Employee.Salary.CurrencyCode).NotEmpty().Length(3);
        }
    }
    public class UpdateEmployeeCommandHandler(EmployeesDbContext dbContext) : ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResult>
    {
        public async Task<UpdateEmployeeResult> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (employee == null)
                throw new EmployeeNotFoundException(command.Id);

            UpdateExistingEmployee(employee, command.Employee);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateEmployeeResult(true);

        }

        private void UpdateExistingEmployee(Employee employee, UpdateEmployeeDto dto)
        {
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Department = dto.Department;
            employee.Status = Enum.Parse<EmployeeStatus>(dto.Status, true);
            employee.Salary = Money.Of(dto.Salary.AmountMinor, dto.Salary.CurrencyCode);
            employee.CustomData = dto.CustomData != null
                ? JsonDocument.Parse(JsonSerializer.Serialize(dto.CustomData))
                : null;
        }
    }
}
