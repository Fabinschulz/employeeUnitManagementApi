using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using MediatR;

namespace EmployeeUnitManagementApi.src.Infra.Services.Extensions
{
    /// <summary>
    /// Provides extension methods for Employee operations.
    /// </summary>
    public static class EmployeeExtension
    {
        /// <summary>
        /// Maps the Employee endpoints to the web application.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void MapEmployeeEndpoints(this WebApplication app)
        {
            app.MapPost("/v1/employee/register", async (IMediator mediator, CreateEmployeeCommand command) =>
            {
                var employee = await mediator.Send(command);
                return Results.Created($"/v1/employee/{employee.Id}", employee);
            }).WithTags("EMPLOYEE").WithSummary("Create a new employee").WithOpenApi();

            // app.MapPut("/v1/employee/{id}", async (IMediator mediator, Guid id, [FromBody] UpdateEmployeeCommand command) =>
            // {
            //     var updatedCommand = new UpdateEmployeeCommand(id, command.Name, command.Email, command.Phone, command.Status);
            //     var employee = await mediator.Send(updatedCommand);
            //     return Results.Ok(employee);
            // }).WithTags("EMPLOYEE").WithSummary("Update an employee");

            // app.MapGet("/v1/employee/{id}", async (IMediator mediator, Guid id) =>
            // {
            //     var command = new GetEmployeeByIdCommand(id);
            //     var employee = await mediator.Send(command);
            //     return Results.Ok(employee);
            // }).WithTags("EMPLOYEE").WithSummary("Find an employee by id");

            // app.MapDelete("/v1/employee/{id}", async (IMediator mediator, Guid id) =>
            // {
            //     var command = new DeleteEmployeeCommand(id);
            //     var employee = await mediator.Send(command);
            //     return Results.Ok(employee);
            // }).WithTags("EMPLOYEE").WithSummary("Delete an employee").RequireAuthorization("Admin");

            // app.MapGet("/v1/employee", async (IMediator mediator, string? name, string? email, string? phone, StatusEnum? status, int page = 0, int size = 20) =>
            // {
            //     var getAllEmployeeRequest = new GetAllEmployeeCommand(page, size, name, email, phone, status);
            //     var employees = await mediator.Send(getAllEmployeeRequest);
            //     return Results.Ok(employees);
            // }).WithTags("EMPLOYEE").WithSummary("Get all employees");
        }
    }
}
