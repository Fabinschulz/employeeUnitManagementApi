using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.UserHandler
{
    /// <summary>
    /// Handles user login commands.
    /// </summary>
    public sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserQuery>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginUserCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginUserHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="validator">The validator.</param>
        public LoginUserHandler(IMapper mapper, IUserRepository userRepository, IValidator<LoginUserCommand> validator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the login user command.
        /// </summary>
        /// <param name="request">The login user command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the login user query.</returns>
        public async Task<LoginUserQuery> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = await _userRepository.Login(request.Email, request.Password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            var userQuery = _mapper.Map<LoginUserQuery>(user);
            return userQuery;
        }
    }
}
