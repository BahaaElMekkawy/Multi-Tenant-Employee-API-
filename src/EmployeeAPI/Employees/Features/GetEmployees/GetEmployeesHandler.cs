using Shared.Pagination;

namespace EmployeeAPI.Employees.Features.GetEmployees
{

    public record GetEmployeesQuery(PaginationRequest PaginationRequest) : IQuery<GetEmployeesResult>;
    public record GetEmployeesResult(PaginatedResult<EmployeeDto> employees);
    public class GetEmployeesHandler(EmployeesDbContext dbContext) : IRequestHandler<GetEmployeesQuery, GetEmployeesResult>
    {
        public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.pageIndex;
            var pageSize = query.PaginationRequest.pageSize;

            var totalCount = await dbContext.Employees.LongCountAsync(cancellationToken);
            var employees = await dbContext.Employees
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var employeesDtos = employees.Adapt<List<EmployeeDto>>();

            return new GetEmployeesResult(new PaginatedResult<EmployeeDto>(pageIndex, pageSize, totalCount, employeesDtos));
        }
    }
}
