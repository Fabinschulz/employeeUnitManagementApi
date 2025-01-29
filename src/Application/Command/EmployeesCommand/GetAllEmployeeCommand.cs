using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand
{
    /// <summary>
    /// Command to get all employee with pagination and filtering options.
    /// </summary>
    /// <param name="Page">The page number.</param>
    /// <param name="Size">The size of the page.</param>
    /// <param name="Name">The Name to filter by.</param>
    /// <param name="OrderBy">The field to order by.</param>
    public sealed record GetAllEmployeeCommand(int Page, int Size, string? Name, string? OrderBy) : IRequest<GetAllEmployeeQuery>;

    /// <summary>
    /// Validator for GetAllEmployeeCommand.
    /// </summary>
    public sealed class GetAllEmployeeValidator : AbstractValidator<GetAllEmployeeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmployeeValidator"/> class.
        /// </summary>
        public GetAllEmployeeValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0).WithMessage("A página precisa ser maior ou igual a 0");

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size é obrigatório")
                .GreaterThan(0).WithMessage("Size precisa ser maior que 0");
        }
    }

}
