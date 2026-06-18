using Shared.Pagination;

namespace EmployeeAPI.Employees.Features.GetEmployees
{

    public record GetEmployeesRequest(PaginationRequest PaginationRequest);
    public record GetEmployeesResponse(PaginatedResult<EmployeeDto> Employees);
    public class GetEmployeesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/v1/employees", async ([AsParameters] PaginationRequest request, ISender sender) => 
            {
                var query = new GetEmployeesQuery(request);
                var result = await sender.Send(query);
                var response = result.Adapt<GetEmployeesResponse>();

                return Results.Ok(response);
            }).WithName("GetEmployees")
              .Produces<GetEmployeesResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Employees")
              .WithDescription("Get All Employees");
        }
    }
}
