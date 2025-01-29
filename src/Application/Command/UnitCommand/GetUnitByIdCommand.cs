using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UnitCommand
{

    /// <summary>
    /// Command to get an unit by ID.
    /// </summary>
    /// <param name="Id">The ID of the unit.</param>
    public sealed record GetUnitByIdCommand(Guid Id) : IRequest<GetUnitByIdQuery>;

    /// <summary>
    /// Validator for the GetUnitByIdCommand.
    /// </summary>
    public sealed class GetUnitByIdValidator : AbstractValidator<GetUnitByIdCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUnitByIdValidator"/> class.
        /// </summary>
        public GetUnitByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => IsGuidValid(id)).WithMessage("Unidade inválida.");
        }

        private bool IsGuidValid(Guid id)
        {
            return Guid.TryParse(id.ToString(), out _);
        }
    }
}
