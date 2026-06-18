namespace EmployeeAPI.Employees.Features.UpdateEmployee
{
    public record UpdateEmployeeRequest(UpdateEmployeeDto Employee);
    public record UpdateEmployeeResponse(bool IsSuccess);
    public class UpdateEmployeeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/v1/employees/{id:guid}", async (Guid id, UpdateEmployeeRequest request, ISender sender) =>
            {
                // Bind the route ID together with the request payload into the Command
                var command = new UpdateEmployeeCommand(id, request.Employee);

                var result = await sender.Send(command);

                return Results.Ok(new UpdateEmployeeResponse(result.IsSuccess));
            })
            .WithName("UpdateEmployee")
            .Produces<UpdateEmployeeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Employee")
            .WithDescription("Update an existing employee's details safely inside tenant boundary context");
        }
    }
}
