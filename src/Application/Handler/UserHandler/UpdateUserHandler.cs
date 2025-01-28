using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using EmployeeUnitManagementApi.src.Application.Common.Exceptions;
using MediatR;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;

namespace EmployeeUnitManagementApi.src.Application.Handler.UserHandler
{
    /// <summary>
    /// Handles the update user command.
    /// </summary>
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly IValidator<UpdateUserCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="validator">The validator.</param>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, ILogger<UpdateUserHandler> logger, IValidator<UpdateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        /// <summary>
        /// Handles the update user command.
        /// </summary>
        /// <param name="request">The update user command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated user query.</returns>
        public async Task<UpdateUserQuery> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            var user = await _userRepository.GetById(request.Id);
            EnsureUserExists(user, request.Id);
            if (request.CurrentPassword != null && request.NewPassword != null) await NewPassw(request);
            UpdateUserProperties(user, request);
            await _userRepository.Update(user);

            var userResponse = MapToUserResponse(user);
            return userResponse;
        }
        private async Task ValidateRequest(UpdateUserCommand request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private void EnsureUserExists(User user, Guid userId)
        {
            if (user == null)
            {
                string errorMessage = $"Usuário com id: {userId} não foi encontrado no banco de dados.";
                _logger.LogError(errorMessage);
                throw new NotFoundException(errorMessage);
            }
        }

        private void UpdateUserProperties(User user, UpdateUserCommand request)
        {
            user.Email = request.Email;
            user.Role = request.Role;
            user.Status = request.Status;
        }

        private Task<User> NewPassw(UpdateUserCommand request)
        {
            return _userRepository.ChangePassword(request.Id, request.CurrentPassword!, request.NewPassword!);
        }

        private UpdateUserQuery MapToUserResponse(User user)
        {
            return _mapper.Map<UpdateUserQuery>(user);
        }

    }
}
