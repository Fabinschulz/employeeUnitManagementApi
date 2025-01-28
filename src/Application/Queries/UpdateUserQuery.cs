namespace EmployeeUnitManagementApi.src.Application.Queries
{
    /// <summary>
    /// Represents a query to update a user.
    /// </summary>
    public sealed record UpdateUserQuery
    {
        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        public string Email { get; init; } = null!;

        /// <summary>
        /// Gets the role of the user.
        /// </summary>
        public string? Role { get; init; }

        /// <summary>
        /// Gets the status of the user.
        /// </summary>
        public string? Status { get; init; }
    }
}
