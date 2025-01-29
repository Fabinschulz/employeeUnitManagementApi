using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand
{
    /// <summary>
    /// Command to update an employee.
    /// </summary>
    /// <param name="Id">The ID of the employee.</param>
    /// <param name="Name">The name of the employee.</param>
    /// <param name="UnitId">The ID of the unit.</param>
    /// <returns>The updated employee query.</returns>
    public sealed record UpdateEmployeeCommand(Guid Id, string Name, Guid UnitId) : IRequest<UpdateEmployeeQuery>;

    /// <summary>
    /// Validator for the UpdateUserCommand.
    /// </summary>
    public sealed class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmployeeValidator"/> class.
        /// </summary>
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O campo 'Name' é obrigatório.");
            RuleFor(x => x.UnitId).NotEmpty().WithMessage("O campo 'Unidade' é obrigatório.");
        }
    }
}
