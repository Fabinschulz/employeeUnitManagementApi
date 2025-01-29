namespace EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries
{
    /// <summary>
    /// Represents a query to create an employee.
    /// </summary>
    public sealed record CreateEmployeeQuery
    {
        /// <summary>
        /// Gets the unique identifier of the employee.
        /// </summary>
        public Guid Id { get; init; }
    }
}
