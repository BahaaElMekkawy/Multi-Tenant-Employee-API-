using Carter;
using EmployeeAPI.Employees.Dtos;
using Mapster;
using MediatR;

namespace EmployeeAPI.Employees.Features.AddEmployee
{
    public record CreateEmployeeRequest(EmployeeDto Employee);
    public record CreateEmployeeResponse(Guid Id);
    public class CreateEmployeeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/v1/employees" , async (CreateEmployeeRequest request , ISender sender) =>
            {
                var command = request.Adapt<CreateEmployeeCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateEmployeeResponse>();

                return Results.Created($"/v1/employees/{response.Id}", response);
            }).WithName("CreateEmployee")
            .Produces<CreateEmployeeResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Employee")
            .WithDescription("Create Employee");
        }
    }
}
