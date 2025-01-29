using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UnitMapper
{
    /// <summary>
    /// Mapper profile for getting all unit.
    /// </summary>
    public sealed class GetAllUnitMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUnitMapper"/> class.
        /// </summary>
        public GetAllUnitMapper()
        {
            CreateMap<GetAllUnitCommand, Unit>();
            CreateMap<Unit, GetAllUnitQuery>();
        }
    }
}
