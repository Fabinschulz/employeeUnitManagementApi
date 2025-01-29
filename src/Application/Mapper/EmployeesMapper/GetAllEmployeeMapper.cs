using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.EmployeesMapper
{
    /// <summary>
    /// Mapper profile for getting all employees.
    /// </summary>
    public sealed class GetAllEmployeeMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmployeeMapper"/> class.
        /// </summary>
        public GetAllEmployeeMapper()
        {
            CreateMap<GetAllEmployeeCommand, Employee>();
            CreateMap<Employee, GetAllEmployeeQuery>();
        }
    }
}
