using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UserMapper
{
    /// <summary>
    /// Mapper profile for creating a user.
    /// </summary>
    public sealed class CreateUserMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserMapper"/> class.
        /// </summary>
        public CreateUserMapper()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, CreateUserQuery>();
        }
    }
}
