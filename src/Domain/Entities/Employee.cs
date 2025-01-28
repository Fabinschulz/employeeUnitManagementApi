using EmployeeUnitManagementApi.src.Domain.Common;

namespace EmployeeUnitManagementApi.src.Domain.Entities
{
    /// <summary>
    /// Represents an employee entity.
    /// </summary>
    public class Employee : BaseEntity
    {

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unit ID associated with the employee.
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Gets or sets the unit associated with the employee.
        /// </summary>
        public Unit? Unit { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the employee.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the employee.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
