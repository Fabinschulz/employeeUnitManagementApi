using AutoMapper;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using EmployeeUnitManagementApi.src.Application.Common.Exceptions;
using MediatR;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UserQueries;

namespace EmployeeUnitManagementApi.src.Application.Handler.UserHandler
{
    /// <summary>
    /// Handler for getting a user by ID.
    /// </summary>
    public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdCommand, GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetUserByIdCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper, IValidator<GetUserByIdCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the request to get a user by ID.
        /// </summary>
        /// <param name="request">The request containing the user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user details.</returns>
        public async Task<GetUserByIdQuery> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetById(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<GetUserByIdQuery>(user);
        }
    }
}
