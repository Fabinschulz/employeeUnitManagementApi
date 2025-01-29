using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Common.Exceptions;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.EmployeesHandler
{
    /// <summary>
    /// Handles the update employee command.
    /// </summary>
    public sealed class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeQuery>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<UpdateEmployeeCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmployeeHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="validator">The validator.</param>
        public UpdateEmployeeHandler(IMapper mapper, IEmployeeRepository employeeRepository, IValidator<UpdateEmployeeCommand> validator)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the update employee command.
        /// </summary>
        /// <param name="request">The update employee command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated employee query.</returns>
        public async Task<UpdateEmployeeQuery> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var employee = await _employeeRepository.GetById(request.Id);
            employee = _mapper.Map(request, employee);
            await _employeeRepository.Update(employee);

            return _mapper.Map<UpdateEmployeeQuery>(employee);
        }

    }
}
