using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UserQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UserMapper
{
    /// <summary>
    /// Mapper profile for updating user information.
    /// </summary>
    public sealed class UpdateUserMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserMapper"/> class.
        /// </summary>
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UpdateUserQuery>();
        }
    }
}
