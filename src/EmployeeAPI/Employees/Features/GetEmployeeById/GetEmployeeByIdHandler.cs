using EmployeeAPI.Employees.Dtos;
using EmployeeAPI.Exceptions;
using Mapster;
using Shared.CQRS;

namespace EmployeeAPI.Employees.Features.GetEmployeeById
{
    public record GetEmployeeByIdQuery(Guid Id) : IQuery<GetEmployeeByIdResult>;
    public record GetEmployeeByIdResult(EmployeeDto Employee);

    public class GetEmployeeByIdCommandHandler(EmployeesDbContext dbContext) : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResult>
    {
        public async Task<GetEmployeeByIdResult> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

            if (employee == null)
                throw new EmployeeNotFoundException(query.Id);

            var result = employee.Adapt<EmployeeDto>();

            return new GetEmployeeByIdResult(result);
        }
    }
}
