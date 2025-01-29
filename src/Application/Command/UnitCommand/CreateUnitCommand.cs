using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UnitCommand
{
    /// <summary>
    /// Command to create a unit.
    /// </summary>
    /// <param name="Name">The name of the unit.</param>
    public sealed record CreateUnitCommand(string Name) : IRequest<CreateUnitQuery>;

    /// <summary>
    /// Validator for the CreateUnitCommand.
    /// </summary>
    public sealed class CreateUnitValidator : AbstractValidator<CreateUnitCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUnitValidator"/> class.
        /// </summary>
        public CreateUnitValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O campo 'Nome' é obrigatório.").ChildRules(name =>
            {
                name.RuleFor(x => x).MaximumLength(80).WithMessage("Nome deve ter no máximo 50 caracteres.");
            });
        }
    }
}
