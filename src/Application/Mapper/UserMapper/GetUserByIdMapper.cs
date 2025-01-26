using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UserMapper
{
    /// <summary>
    /// Mapper profile for getting a user by ID.
    /// </summary>
    public sealed class GetUserByIdMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdMapper"/> class.
        /// </summary>
        public GetUserByIdMapper()
        {
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, GetUserByIdQuery>();
        }
    }
}
