﻿using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeUnitManagementApi.src.Infra.Services.Extensions
{
    /// <summary>
    /// Provides extension methods for user-related operations.
    /// </summary>
    public static class UserExtension
    {
        /// <summary>
        /// Maps the user endpoints to the web application.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapPost("/v1/user/register", async (IMediator mediator, CreateUserCommand command) =>
            {
                var user = await mediator.Send(command);
                return Results.Created($"/v1/user/{user.Id}", user);
            }).WithTags("USER").WithSummary("Create a new user").WithOpenApi();

            app.MapPost("/v1/user/login", async (IMediator mediator, LoginUserCommand command) =>
            {
                var user = await mediator.Send(command);
                return Results.Ok(user);
            }).WithTags("USER").WithSummary("Login a user");

            app.MapPut("/v1/user/{id}", async (IMediator mediator, Guid id, [FromBody] UpdateUserCommand command) =>
            {
                var updatedCommand = new UpdateUserCommand(id, command.Username, command.Email, command.Role, command.Status);
                var user = await mediator.Send(updatedCommand);
                return Results.Ok(user);
            }).WithTags("USER").WithSummary("Update a user");

            app.MapGet("/v1/user/{id}", async (IMediator mediator, Guid id) =>
            {
                var command = new GetUserByIdCommand(id);
                var user = await mediator.Send(command);
                return Results.Ok(user);
            }).WithTags("USER").WithSummary("Find a user by id");

            app.MapDelete("/v1/user/{id}", async (IMediator mediator, Guid id) =>
            {
                var command = new DeleteUserCommand(id);
                var user = await mediator.Send(command);
                return Results.Ok(user);
            }).WithTags("USER").WithSummary("Delete a user").RequireAuthorization("Admin");

            app.MapGet("/v1/user", async (IMediator mediator, int page, int size, string? username, string? email, string? orderBy, StatusEnum? status, RoleEnum? role) =>
            {
                var getAllUserRequest = new GetAllUserCommand(page, size, username, email, orderBy, status, role);
                var users = await mediator.Send(getAllUserRequest);
                return Results.Ok(users);
            }).WithTags("USER").WithSummary("Get all users");
        }
    }
}
