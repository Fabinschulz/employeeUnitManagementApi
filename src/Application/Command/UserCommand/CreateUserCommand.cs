using EmployeeUnitManagementApi.src.Application.Queries.UserQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UserCommand
{
    /// <summary>
    /// Command to create a new user.
    /// </summary>
    /// <param name="Email">The email of the user.</param>
    /// <param name="Password">The password of the user.</param>
    public sealed record CreateUserCommand(string Email, string Password) : IRequest<CreateUserQuery>;

    /// <summary>
    /// Validator for the CreateUserCommand.
    /// </summary>
    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
        /// </summary>
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório.").ChildRules(email =>
            {
                email.RuleFor(x => x).EmailAddress().WithMessage("Email inválido.");
                email.RuleFor(x => x).MaximumLength(50).WithMessage("Email deve ter no máximo 50 caracteres.");
            });
            RuleFor(x => x.Password).NotEmpty().WithMessage("Senha é obrigatória.").ChildRules(password =>
            {
                password.RuleFor(x => x).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Senha deve ter no mínimo 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
            });
        }
    }

}
