using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UnitCommand
{
    /// <summary>
    /// Command to get all unit with pagination and filtering options.
    /// </summary>
    /// <param name="Page">The page number.</param>
    /// <param name="Size">The size of the page.</param>
    /// <param name="Name">The Name to filter by.</param>
    /// <param name="OrderBy">The field to order by.</param>
    public sealed record GetAllUnitCommand(int Page, int Size, string? Name, string? OrderBy) : IRequest<GetAllUnitQuery>;

    /// <summary>
    /// Validator for GetAllUnitCommand.
    /// </summary>
    public sealed class GetAllUnitValidator : AbstractValidator<GetAllUnitCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUnitValidator"/> class.
        /// </summary>
        public GetAllUnitValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0).WithMessage("A página precisa ser maior ou igual a 0");

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size é obrigatório")
                .GreaterThan(0).WithMessage("Size precisa ser maior que 0");
        }
    }
}
