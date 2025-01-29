using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand
{
    /// <summary>
    /// Command to delete a employee by Id.
    /// </summary>
    /// <param name="Id">The Id of the user to be deleted.</param>
    public sealed record DeleteEmployeeByIdCommand(Guid Id) : IRequest<DeleteEmployeeByIdQuery>;
}
