using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.UnitHandler
{

    /// <summary>
    /// Handler for getting a unit by its ID.
    /// </summary>
    public sealed class GetUnitByIdHandler : IRequestHandler<GetUnitByIdCommand, GetUnitByIdQuery>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetUnitByIdCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUnitByIdHandler"/> class.
        /// </summary>
        /// <param name="unitRepository">The unit repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public GetUnitByIdHandler(IUnitRepository unitRepository, IMapper mapper, IValidator<GetUnitByIdCommand> validator)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the request to get a unit by its ID.
        /// </summary>
        /// <param name="request">The request containing the unit ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unit query.</returns>
        public async Task<GetUnitByIdQuery> Handle(GetUnitByIdCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var unit = await _unitRepository.GetById(request.Id);
            return _mapper.Map<GetUnitByIdQuery>(unit);
        }

    }
}
