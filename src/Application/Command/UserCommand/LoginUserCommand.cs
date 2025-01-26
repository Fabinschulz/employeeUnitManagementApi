using EmployeeUnitManagementApi.src.Application.Queries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UserCommand
{
    /// <summary>
    /// Command to log in a user.
    /// </summary>
    /// <param name="Email">The email of the user.</param>
    /// <param name="Password">The password of the user.</param>
    public sealed record LoginUserCommand(string Email, string Password) : IRequest<LoginUserQuery>;

    /// <summary>
    /// Validator for the LoginUserCommand.
    /// </summary>
    public sealed class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginUserValidator"/> class.
        /// </summary>
        public LoginUserValidator()
        {

            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email é obrigatório.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }

}
