
namespace EmployeeAPI.Employees.Features.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid Id) : ICommand<DeleteEmployeeResult>;
    public record DeleteEmployeeResult(bool IsSuccess);
    public class DeleteEmployeeHandler(EmployeesDbContext dbContext) : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        public async Task<DeleteEmployeeResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FindAsync(command.Id, cancellationToken);

            if (employee is null)
                throw new EmployeeNotFoundException(command.Id);

            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteEmployeeResult(true);
        }
    }
}
