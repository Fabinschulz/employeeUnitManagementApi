using EmployeeUnitManagementApi.src.Domain.Common;

namespace EmployeeUnitManagementApi.src.Application.Queries.UserQueries
{

    /// <summary>
    /// Represents a query to get a user by their ID.
    /// </summary>
    public sealed class GetUserByIdQuery : BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier for the user.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        public string? Email { get; }

        /// <summary>
        /// Gets the role of the user (Admin, User).
        /// </summary>
        public string? Role { get; }

        /// <summary>
        /// Gets the status of the user (Active, Inactive).
        /// </summary>
        public string? Status { get; }

    }

}
