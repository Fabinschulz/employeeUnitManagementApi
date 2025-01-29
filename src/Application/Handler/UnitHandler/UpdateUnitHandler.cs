using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.UnitHandler
{
    /// <summary>
    /// Handles the update unit command.
    /// </summary>
    public sealed class UpdateUnitHandler : IRequestHandler<UpdateUnitCommand, UpdateUnitQuery>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateUnitCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnitHandler"/> class.
        /// </summary>
        /// <param name="unitRepository">The unit repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public UpdateUnitHandler(IUnitRepository unitRepository, IMapper mapper, IValidator<UpdateUnitCommand> validator)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the update unit command.
        /// </summary>
        /// <param name="request">The update unit command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated unit query.</returns>
        public async Task<UpdateUnitQuery> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var unit = await _unitRepository.GetById(request.Id);
            unit.Name = request.Name;
            unit.Status = request.Status;

            await _unitRepository.Update(unit);
            return _mapper.Map<UpdateUnitQuery>(unit);
        }
    }
}
