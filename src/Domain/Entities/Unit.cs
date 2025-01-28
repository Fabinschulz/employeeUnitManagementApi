using EmployeeUnitManagementApi.src.Domain.Common;

namespace EmployeeUnitManagementApi.src.Domain.Entities
{
    /// <summary>
    /// Represents a unit entity.
    /// </summary>
    public class Unit : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the employees of the unit.
        /// </summary>
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
