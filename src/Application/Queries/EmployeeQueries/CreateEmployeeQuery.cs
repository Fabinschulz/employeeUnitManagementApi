using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries
{
    /// <summary>
    /// Represents a query to create an employee.
    /// </summary>
    public sealed record CreateEmployeeQuery
    {
        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the name of the employee.
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// Gets the unit vinculated to the employee.
        /// </summary>
        public Unit Unit { get; init; } = null!;

        /// <summary>
        /// Gets the user vinculated to the employee.
        /// </summary>
        public User user { get; init; } = null!;
    }
}
