using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Handler.UserHandler
{
    /// <summary>
    /// Handler for getting all users.
    /// </summary>
    public sealed class GetAllUserHandler : IRequestHandler<GetAllUserCommand, GetAllUserQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetAllUserCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="validator">The validator.</param>
        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper, IValidator<GetAllUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Handles the request to get all users.
        /// </summary>
        /// <param name="request">The request command containing the parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the query result.</returns>
        public async Task<GetAllUserQuery> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var users = await _userRepository.GetAll(
                request.Page,
                request.Size,
                request.Email,
                request.OrderBy,
                request.Status,
                request.Role
                );

            var usersMapped = _mapper.Map<ListDataPagination<User>>(users);
            return _mapper.Map<GetAllUserQuery>(usersMapped);
        }
    }
}
