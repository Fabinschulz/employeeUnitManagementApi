using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UserCommand;
using EmployeeUnitManagementApi.src.Application.Queries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UserMapper
{
    /// <summary>
    /// Mapper profile for getting all users.
    /// </summary>
    public sealed class GetAllUserMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUserMapper"/> class.
        /// </summary>
        public GetAllUserMapper()
        {
            CreateMap<GetAllUserCommand, User>();
            CreateMap<User, GetAllUserQuery>();
        }
    }
}
