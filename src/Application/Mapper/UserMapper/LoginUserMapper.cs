using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Queries.UserQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UserMapper
{
    /// <summary>
    /// Mapper profile for mapping User to LoginUserQuery.
    /// </summary>
    public sealed class LoginUserMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginUserMapper"/> class.
        /// </summary>
        public LoginUserMapper()
        {
            CreateMap<User, LoginUserQuery>()
              .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));
        }
    }
}
