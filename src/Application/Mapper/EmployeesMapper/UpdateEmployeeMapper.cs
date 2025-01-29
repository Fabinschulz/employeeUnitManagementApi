using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.EmployeesMapper
{
    /// <summary>
    /// Mapper profile for updating employee information.
    /// </summary>
    public sealed class UpdateEmployeeMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmployeeMapper"/> class.
        /// </summary>
        public UpdateEmployeeMapper()
        {
            CreateMap<UpdateEmployeeCommand, Employee>();
            CreateMap<Employee, UpdateEmployeeQuery>();
        }
    }
}
