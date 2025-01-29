using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UnitMapper
{
    /// <summary>
    /// Mapper profile for getting a unit by ID.
    /// </summary>
    public sealed class GetUnitByIdMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUnitByIdMapper"/> class.
        /// </summary>
        public GetUnitByIdMapper()
        {
            CreateMap<GetUnitByIdCommand, Unit>();
            CreateMap<Unit, GetUnitByIdQuery>();
        }
    }
}
