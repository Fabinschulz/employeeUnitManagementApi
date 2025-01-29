using AutoMapper;
using EmployeeUnitManagementApi.src.Application.Command.UnitCommand;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Mapper.UnitMapper
{
    /// <summary>
    /// Mapper profile for updating unit information.
    /// </summary>
    public sealed class UpdateUnitMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnitMapper"/> class.
        /// </summary>
        public UpdateUnitMapper()
        {
            CreateMap<UpdateUnitCommand, Unit>();
            CreateMap<Unit, UpdateUnitQuery>();
        }
    }
}
