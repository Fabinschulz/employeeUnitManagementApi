namespace EmployeeUnitManagementApi.src.Application.Queries.UnitQueries
{
    /// <summary>
    /// Represents a query to create an unit.
    /// </summary>
    public sealed record CreateUnitQuery
    {
        /// <summary>
        /// Gets the unique identifier of the unit.
        /// </summary>
        public Guid Id { get; init; }
    }
}
