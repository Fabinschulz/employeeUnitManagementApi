using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeUnitManagementApi.src.Infra.Services.Extensions
{
    /// <summary>
    /// Provides extension methods for unit operations.
    /// </summary>
    public static class UnitExtension
    {
        /// <summary>
        /// Maps the unit endpoints to the web application.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void MapUnitEndpoints(this WebApplication app)
        {
            app.MapPost("/v1/unit/register", async (IMediator mediator, CreateUnitCommand command) =>
            {
                var unit = await mediator.Send(command);
                return Results.Created($"/v1/unit/{unit.Id}", unit.Id);
            }).WithTags("UNIT").WithSummary("Create a new unit").WithOpenApi();

            app.MapPut("/v1/unit/{id}", async (IMediator mediator, Guid id, [FromBody] UpdateUnitCommand command) =>
            {
                var updatedCommand = new UpdateUnitCommand(id, command.Name, command.Status);
                var unit = await mediator.Send(updatedCommand);
                return Results.Ok(unit);
            }).WithTags("UNIT").WithSummary("Update a unit").WithOpenApi();

            app.MapGet("/v1/unit/{id}", async (IMediator mediator, Guid id) =>
            {
                var command = new GetUnitByIdCommand(id);
                var unit = await mediator.Send(command);
                return Results.Ok(unit);
            }).WithTags("UNIT").WithSummary("Find a unit by id").WithOpenApi();

            app.MapDelete("/v1/unit/{id}", async (IMediator mediator, Guid id) =>
            {
                var command = new DeleteUnitByIdCommand(id);
                var unit = await mediator.Send(command);
                return Results.Ok(unit);
            }).WithTags("UNIT").WithSummary("Delete a unit").WithOpenApi();
            // .RequireAuthorization("Admin");

            app.MapGet("/v1/unit", async (IMediator mediator, string? name, string? orderBy, int page = 0, int size = 20) =>
            {
                var getAllUnitRequest = new GetAllUnitCommand(page, size, name, orderBy);
                var units = await mediator.Send(getAllUnitRequest);
                return Results.Ok(units);
            }).WithTags("UNIT").WithSummary("Get all units").WithOpenApi();
        }
    }
}
