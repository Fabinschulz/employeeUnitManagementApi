using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Enums;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;

namespace EmployeeUnitManagementApi.src.Application.Command.UserCommand
{
    /// <summary>
    /// Command to update a user.
    /// </summary>
    /// <param name="Id">The unique identifier of the user.</param>
    /// <param name="Username">The username of the user.</param>
    /// <param name="Email">The email address of the user.</param>
    /// <param name="Role">The role of the user.</param>
    /// <param name="Status">The status of the user.</param>
    public sealed record UpdateUserCommand(
        Guid Id,
        string Username,
        string Email,
        [property: JsonConverter(typeof(RoleEnumConverter))] RoleEnum Role,
        [property: JsonConverter(typeof(StatusEnumConverter))] StatusEnum Status
    ) : IRequest<UpdateUserQuery>;

    /// <summary>
    /// Validator for the UpdateUserCommand.
    /// </summary>
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserValidator"/> class.
        /// </summary>
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.Status).NotEmpty().IsInEnum();
        }
    }
}
