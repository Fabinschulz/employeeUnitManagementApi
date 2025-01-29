

using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using UnitEntity = EmployeeUnitManagementApi.src.Domain.Entities.Unit;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.UnitHandler
{
    /// <summary>
    /// Handler for creating an unit.
    /// </summary>
    public sealed class CreateUnitHandler : IRequestHandler<CreateUnitCommand, CreateUnitQuery>
    {
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;
        private readonly IValidator<CreateUnitCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUnitHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitRepository">The unit repository.</param>
        /// <param name="validator">The validator.</param>
        public CreateUnitHandler(IMapper mapper, IUnitRepository unitRepository, IValidator<CreateUnitCommand> validator)
        {
            _mapper = mapper;
            _unitRepository = unitRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the creation of a unit.
        /// </summary>
        /// <param name="request">The create unit command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created unit query.</returns>
        public async Task<CreateUnitQuery> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var mappedUnit = MapUnit(request);
            var registered = await Register(mappedUnit);
            var response = MapUnitToResponse(registered);

            return response;
        }

        private async Task ValidateRequest(CreateUnitCommand request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private UnitEntity MapUnit(CreateUnitCommand request)
        {
            return _mapper.Map<UnitEntity>(request);
        }

        private async Task<UnitEntity> Register(UnitEntity unit)
        {
            return await _unitRepository.Create(unit);
        }

        private CreateUnitQuery MapUnitToResponse(UnitEntity unit)
        {
            return _mapper.Map<CreateUnitQuery>(unit);
        }

    }
}
