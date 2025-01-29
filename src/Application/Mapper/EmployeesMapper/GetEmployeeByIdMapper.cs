using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.EmployeesMapper
{
    /// <summary>
    /// Mapper profile for getting a employee by ID.
    /// </summary>
    public sealed class GetEmployeeByIdMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeeByIdMapper"/> class.
        /// </summary>
        public GetEmployeeByIdMapper()
        {
            CreateMap<GetEmployeeByIdCommand, Employee>();
            CreateMap<Employee, GetEmployeeByIdQuery>();
        }
    }
}
