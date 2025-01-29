using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UnitCommand
{

    /// <summary>
    /// Command to delete a unit by Id.
    /// </summary>
    /// <param name="Id">The Id of the unit to be deleted.</param>
    public sealed record DeleteUnitByIdCommand(Guid Id) : IRequest<DeleteUnitByIdQuery>;
}
