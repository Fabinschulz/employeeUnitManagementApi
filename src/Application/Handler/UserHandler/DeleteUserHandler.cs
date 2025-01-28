using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UserQueries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using MediatR;


namespace EmployeeUnitManagementApi.src.Application.Handler.UserHandler
{
    /// <summary>
    /// Handler for deleting a user.
    /// </summary>
    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the delete user command.
        /// </summary>
        /// <param name="request">The delete user command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the delete user by id query.</returns>
        public async Task<DeleteUserByIdQuery> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await DeleteUserInRepository(request.Id);

            var message = isDeleted ? "Usuário deletado com sucesso." : "Falha ao deletar o usuário.";
            return new DeleteUserByIdQuery(isDeleted, message);
        }

        private async Task<bool> DeleteUserInRepository(Guid id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
