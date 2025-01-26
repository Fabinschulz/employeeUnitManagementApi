using MediatR;
using FluentValidation;
using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;

namespace EmployeeUnitManagementApi.src.Application.Handler.UserHandler
{
    /// <summary>
    /// Handles the creation of a new user.
    /// </summary>
    public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserQuery>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="validator">The validator instance.</param>
        public CreateUserHandler(IMapper mapper, IUserRepository userRepository, IValidator<CreateUserCommand> validator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the creation of a new user.
        /// </summary>
        /// <param name="request">The create user command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created user query.</returns>
        public async Task<CreateUserQuery> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            await ValidateRequest(request, cancellationToken);

            var mappedUser = mapUser(request);
            var registered = await Register(mappedUser);
            var response = MapUserToResponse(registered);

            return response;
        }

        private async Task ValidateRequest(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
        }

        private User mapUser(CreateUserCommand request)
        {
            return _mapper.Map<User>(request);

        }

        private async Task<User> Register(User user)
        {
            return await _userRepository.Register(user);
        }

        private CreateUserQuery MapUserToResponse(User user)
        {
            return _mapper.Map<CreateUserQuery>(user);
        }


    }
}
