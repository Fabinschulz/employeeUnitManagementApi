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
    /// Handler to get an employee by ID.
    /// </summary>
    public sealed class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdCommand, GetEmployeeByIdQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetEmployeeByIdCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeeByIdHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, IMapper mapper, IValidator<GetEmployeeByIdCommand> validator)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the request to get an employee by ID.
        /// </summary>
        /// <param name="request">The request containing the employee ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the employee details.</returns>
        public async Task<GetEmployeeByIdQuery> Handle(GetEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var employee = await _employeeRepository.GetById(request.Id);
            return _mapper.Map<GetEmployeeByIdQuery>(employee);
        }
    }
}
