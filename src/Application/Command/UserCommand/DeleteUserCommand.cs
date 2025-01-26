using EmployeeUnitManagementApi.src.Application.Queries;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UserCommand
{
    /// <summary>
    /// Command to delete a user by Id.
    /// </summary>
    /// <param name="Id">The Id of the user to be deleted.</param>
    public sealed record DeleteUserCommand(Guid Id) : IRequest<DeleteUserByIdQuery>;
}
