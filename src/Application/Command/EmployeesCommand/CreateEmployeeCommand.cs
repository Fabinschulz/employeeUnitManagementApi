
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand
{
    /// <summary>
    /// Command to create a new employee.
    /// </summary>
    /// <param name="Name">The name of the employee.</param>
    /// <param name="UnitId">The unit ID associated with the employee.</param>
    ///  <param name="UserId">The user ID associated with the employee.</param>
    public sealed record CreateEmployeeCommand(string Name, string UnitId, string UserId) : IRequest<CreateEmployeeQuery>;

    /// <summary>
    /// Validator for the CreateEmployeeCommand.
    /// </summary>
    public sealed class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeValidator"/> class.
        /// </summary>
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O campo 'Nome' é obrigatório.").ChildRules(name =>
            {
                name.RuleFor(x => x).MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caracteres.");
            });
            RuleFor(x => x.UnitId).NotEmpty().WithMessage("O campo 'Unidade' é obrigatório.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("O campo 'Usuário' é obrigatório.");
        }
    }
}
