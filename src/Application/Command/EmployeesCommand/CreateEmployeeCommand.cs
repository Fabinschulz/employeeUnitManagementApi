
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand
{
    /// <summary>
    /// Command to create a new employee.
    /// </summary>
    /// <param name="Name">The name of the employee.</param>
    /// <param name="Unit">The unit of the employee.</param>
    public sealed record CreateEmployeeCommand(string Name, string Unit) : IRequest<CreateEmployeeQuery>;

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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.").ChildRules(name =>
            {
                name.RuleFor(x => x).MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caracteres.");
            });
            RuleFor(x => x.Unit).NotEmpty().WithMessage("Unidade é obrigatória.");
        }
    }
}
