using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.EmployeesHandler
{
    /// <summary>
    /// Handler for creating an employee.
    /// </summary>
    public sealed class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeQuery>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="validator">The validator.</param>
        public CreateEmployeeHandler(IMapper mapper, IEmployeeRepository employeeRepository, IValidator<CreateEmployeeCommand> validator)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the creation of an employee.
        /// </summary>
        /// <param name="request">The create employee command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created employee query.</returns>
        public async Task<CreateEmployeeQuery> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var mappedEmployee = MapEmployee(request);
            var registered = await Register(mappedEmployee);
            var response = MapEmployeeToResponse(registered);

            return response;
        }

        private async Task ValidateRequest(CreateEmployeeCommand request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private Employee MapEmployee(CreateEmployeeCommand request)
        {
            return _mapper.Map<Employee>(request);
        }

        private async Task<Employee> Register(Employee employee)
        {
            return await _employeeRepository.Create(employee);
        }

        private CreateEmployeeQuery MapEmployeeToResponse(Employee employee)
        {
            return _mapper.Map<CreateEmployeeQuery>(employee);
        }

    }
}
