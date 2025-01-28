using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Enums;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmployeeUnitManagementApi.src.Application.Command.UserCommand
{
    /// <summary>
    /// Command to get all users with pagination and filtering options.
    /// </summary>
    /// <param name="Page">The page number.</param>
    /// <param name="Size">The size of the page.</param>
    /// <param name="Email">The email to filter by.</param>
    /// <param name="OrderBy">The field to order by.</param>
    /// <param name="Status">The status to filter by.</param>
    /// <param name="Role">The role to filter by.</param>
    public sealed record GetAllUserCommand(
        int Page,
        int Size,
        string? Email,
        string? OrderBy,
        StatusEnum? Status,
        RoleEnum? Role)
        : IRequest<GetAllUserQuery>;

    // Validator for GetAllUserCommand
    /// <summary>
    /// Validator for GetAllUserCommand.
    /// </summary>
    public sealed class GetAllUserValidator : AbstractValidator<GetAllUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUserValidator"/> class.
        /// </summary>
        public GetAllUserValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0).WithMessage("A página precisa ser maior ou igual a 0");

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size é obrigatório")
                .GreaterThan(0).WithMessage("Size precisa ser maior que 0");

            RuleFor(x => x.Email)
                .Must(email => IsEmailValid(email!)).WithMessage("Email inválido.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status inválido");
        }

        private bool IsEmailValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
