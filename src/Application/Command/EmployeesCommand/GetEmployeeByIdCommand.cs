using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand
{

    /// <summary>
    /// Command to get an employee by ID.
    /// </summary>
    /// <param name="Id">The ID of the employee.</param>
    public sealed record GetEmployeeByIdCommand(Guid Id) : IRequest<GetEmployeeByIdQuery>;

    /// <summary>
    /// Validator for the GetEmployeeByIdCommand.
    /// </summary>
    public sealed class GetEmployeeIdValidator : AbstractValidator<GetEmployeeByIdCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeeIdValidator"/> class.
        /// </summary>
        public GetEmployeeIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => IsGuidValid(id)).WithMessage("Colaborador inválido.");
        }

        private bool IsGuidValid(Guid id)
        {
            return Guid.TryParse(id.ToString(), out _);
        }
    }
}
