using EmployeeUnitManagementApi.src.Domain.Common;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Enums;
using System.Text.Json.Serialization;

namespace EmployeeUnitManagementApi.src.Application.Queries.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object for the User entity.
    /// </summary>
    public class UserDto : BaseEntity
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        [JsonConverter(typeof(RoleEnumConverter))]
        public RoleEnum? Role { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDto"/> class.
        /// </summary>
        /// <param name="user">The user entity.</param>
        public UserDto(User user)
        {
            Email = user.Email;
            Role = user.Role;
            Status = user.Status;
            Id = user.Id;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
            DeletedAt = user.DeletedAt;
        }
    }
}
