
namespace EmployeeAPI.Employees.Features.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid Id) : ICommand<DeleteEmployeeResult>;
    public record DeleteEmployeeResult(bool IsSuccess);
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    public class DeleteEmployeeCommandHandler(EmployeesDbContext dbContext) : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        public async Task<DeleteEmployeeResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (employee is null)
                throw new EmployeeNotFoundException(command.Id);

            employee.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteEmployeeResult(true);
        }
    }
}
