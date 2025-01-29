using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;

namespace EmployeeUnitManagementApi.src.Application.Handler.UnitHandler
{
    /// <summary>
    /// Handler for getting all unit.
    /// </summary>
    public sealed class GetAllUnitHandler : MediatR.IRequestHandler<GetAllUnitCommand, GetAllUnitQuery>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetAllUnitCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUnitHandler"/> class.
        /// </summary>
        /// <param name="unitRepository">The unit repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public GetAllUnitHandler(IUnitRepository unitRepository, IMapper mapper, IValidator<GetAllUnitCommand> validator)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the request to get all units.
        /// </summary>
        /// <param name="request">The request to get all units.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the query with all units.</returns>

        public async Task<GetAllUnitQuery> Handle(GetAllUnitCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var units = await _unitRepository.GetAll(
                request.Page,
                request.Size,
                request.Name,
                request.OrderBy
                );

            var unitsMapped = _mapper.Map<ListDataPagination<Unit>>(units);
            return _mapper.Map<GetAllUnitQuery>(unitsMapped);
        }

    }
}
