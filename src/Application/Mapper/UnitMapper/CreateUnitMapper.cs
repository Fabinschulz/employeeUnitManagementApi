using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UnitMapper
{
    /// <summary>
    /// Mapper profile for creating an unit.
    /// </summary>
    public sealed class CreateUnitMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUnitMapper"/> class.
        /// </summary>
        public CreateUnitMapper()
        {
            CreateMap<CreateUnitCommand, Unit>();
            CreateMap<Unit, CreateUnitQuery>();
        }
    }
}
