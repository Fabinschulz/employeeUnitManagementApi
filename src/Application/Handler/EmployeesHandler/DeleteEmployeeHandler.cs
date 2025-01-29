using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.EmployeesHandler
{
    /// <summary>
    /// Handles the deletion of an employee.
    /// </summary>
    public sealed class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeByIdCommand, DeleteEmployeeByIdQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmployeeHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Handles the delete employee command.
        /// </summary>
        /// <param name="request">The delete employee command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the delete employee by id query.</returns>
        public async Task<DeleteEmployeeByIdQuery> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await DeleteUserInRepository(request.Id);

            var message = isDeleted ? "Colaborador deletado com sucesso." : "Falha ao deletar o colaborador.";
            return new DeleteEmployeeByIdQuery(isDeleted, message);
        }

        private async Task<bool> DeleteUserInRepository(Guid id)
        {
            return await _employeeRepository.Delete(id);
        }
    }
}
