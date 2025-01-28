using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.EmployeesCommand;
using EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.EmployeesMapper
{
    /// <summary>
    /// Mapper profile for creating an employee.
    /// </summary>
    public sealed class CreateEmployeeMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeMapper"/> class.
        /// </summary>
        public CreateEmployeeMapper()
        {
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<Employee, CreateEmployeeQuery>();
        }
    }
}
