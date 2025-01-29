namespace EmployeeUnitManagementApi.src.Application.Queries.UserQueries
{

    /// <summary>
    /// Represents a query to get a user by their ID.
    /// </summary>
    public sealed class GetUserByIdQuery
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


        /// <summary>
        /// Gets the creation date and time of the entity.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Gets or sets the last updated date and time of the entity.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="role">The role of the user (Admin, User).</param>
        /// <param name="status">The status of the user (Active, Inactive).</param>
        /// <param name="createdAt">The creation date and time of the entity.</param>
        /// <param name="updatedAt">The last updated date and time of the entity.</param>
        public GetUserByIdQuery(Guid id, string? email, string? role, string? status, DateTimeOffset createdAt, DateTimeOffset? updatedAt)
        {
            Id = id;
            Email = email;
            Role = role;
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }

}
