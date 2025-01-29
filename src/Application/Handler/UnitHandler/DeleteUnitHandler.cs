using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.UnitHandler
{
    /// <summary>
    /// Handles the deletion of an unit.
    /// </summary>
    public sealed class DeleteUnitHandler : IRequestHandler<DeleteUnitByIdCommand, DeleteUnitByIdQuery>
    {
        private readonly IUnitRepository _unitRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUnitHandler"/> class.
        /// </summary>
        /// <param name="unitRepository">The unit repository.</param>
        public DeleteUnitHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        /// <summary>
        /// Handles the deletion of a unit by its ID.
        /// </summary>
        /// <param name="request">The command containing the ID of the unit to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A query result indicating whether the deletion was successful and a message.</returns>
        public async Task<DeleteUnitByIdQuery> Handle(DeleteUnitByIdCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await DeleteUnitInRepository(request.Id);

            var message = isDeleted ? "Unidade deletada com sucesso." : "Falha ao deletar a unidade.";
            return new DeleteUnitByIdQuery(isDeleted, message);
        }

        private async Task<bool> DeleteUnitInRepository(Guid id)
        {
            return await _unitRepository.Delete(id);
        }
    }
}
