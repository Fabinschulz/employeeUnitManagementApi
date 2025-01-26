using EmployeeUnitManagementApi.src.Domain.Enums;
using System.Text.Json.Serialization;

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
        /// Gets the username of the user.
        /// </summary>
        public string Username { get; init; } = null!;

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        public string Email { get; init; } = null!;

        /// <summary>
        /// Gets the role of the user.
        /// </summary>
        [JsonConverter(typeof(RoleEnumConverter))]
        public RoleEnum Role { get; init; }

        /// <summary>
        /// Gets the status of the user.
        /// </summary>
        [JsonConverter(typeof(StatusEnumConverter))]
        public StatusEnum Status { get; init; }

        /// <summary>
        /// Gets the password of the user.
        /// </summary>
        public string? Password { get; init; }
    }
}
