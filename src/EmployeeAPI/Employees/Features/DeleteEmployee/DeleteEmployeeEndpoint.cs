namespace EmployeeAPI.Employees.Features.DeleteEmployee
{
    public record DeleteEmployeeResponse(bool IsSuccess);

    public class DeleteEmployeeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/v1/employees/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteEmployeeCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteEmployeeResponse>();
                return Results.Ok(response);
            }).WithName("Delete Employee")
              .Produces<DeleteEmployeeResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Delete Employee")
              .WithDescription("Deletes an employee");
        }
    }
}
