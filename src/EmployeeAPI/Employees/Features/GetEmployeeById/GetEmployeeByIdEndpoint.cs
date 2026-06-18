
namespace EmployeeAPI.Employees.Features.GetEmployeeById
{
    public record GetEmployeeByIdResponse(EmployeeDto Employee);

    public class GetEmployeeByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/v1/employees/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetEmployeeByIdQuery(id);
                var result = await sender.Send(query);
                var response = result.Adapt<GetEmployeeByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetEmployeeById")
            .Produces<GetEmployeeByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Employee by Id")
            .WithDescription("Gets an employee by its unique identifier.");
        }
    }
}
