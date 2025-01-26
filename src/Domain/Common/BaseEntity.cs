using System.Text.Json.Serialization;
using EmployeeUnitManagementApi.src.Domain.Enums;

namespace EmployeeUnitManagementApi.src.Domain.Common
{
    /// <summary>
    /// Represents the base entity with common properties.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the entity.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Gets or sets the last updated date and time of the entity.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; } = null;

        /// <summary>
        /// Gets or sets the deletion date and time of the entity.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted.
        /// </summary>
        public bool IsDeleted { get; internal set; } = false;

        /// <summary>
        /// Gets or sets the status of the entity.
        /// </summary>
        [JsonConverter(typeof(StatusEnumConverter))]
        public StatusEnum? Status { get; set; } = StatusEnum.Ativo;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
