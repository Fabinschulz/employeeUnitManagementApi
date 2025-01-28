using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Domain.Interfaces
{
    /// <summary>
    /// Interface for Employee Repository
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {

        /// <summary>
        /// Retrieves a paginated list of employees.
        /// </summary>
        /// <param name="Page">The page number.</param>
        /// <param name="Size">The size of the page.</param>
        /// <param name="Name">The name to filter by.</param>
        /// <param name="OrderBy">The order by criteria.</param>
        /// <returns>A paginated list of employees.</returns>
        Task<ListDataPagination<Employee>> GetAll(int Page, int Size, string? Name, string? OrderBy);
    }
}
