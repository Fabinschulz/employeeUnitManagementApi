using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.EmployeesHandler
{
    /// <summary>
    /// Handler for getting all employees.
    /// </summary>
    public sealed class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeCommand, GetAllEmployeeQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetAllEmployeeCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmployeeHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper, IValidator<GetAllEmployeeCommand> validator)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the request to get all employees.
        /// </summary>
        /// <param name="request">The request command to get all employees.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the query result with all employees.</returns>
        public async Task<GetAllEmployeeQuery> Handle(GetAllEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var employees = await _employeeRepository.GetAll(
                request.Page,
                request.Size,
                request.Name,
                request.OrderBy
                );

            var employeesMapped = _mapper.Map<ListDataPagination<Employee>>(employees);
            return _mapper.Map<GetAllEmployeeQuery>(employeesMapped);
        }

    }
}
