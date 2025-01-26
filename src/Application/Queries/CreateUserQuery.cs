using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Queries
{
    /// <summary>
    /// Represents a query to create a user.
    /// </summary>
    public sealed record CreateUserQuery
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username for the user.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the email for the user.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the role for the user.
        /// </summary>
        public required string Role { get; set; }

        /// <summary>
        /// Gets or sets the status for the user.
        /// </summary>
        public required string Status { get; set; }

        /// <summary>
        /// Converts a User entity to a CreateUserQuery.
        /// </summary>
        /// <param name="user">The User entity to convert.</param>
        /// <returns>A CreateUserQuery representing the User entity.</returns>
        public static implicit operator CreateUserQuery(User user)
        {
            return new CreateUserQuery
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()!,
                Status = user.Status.ToString()!
            };
        }
    }
}
