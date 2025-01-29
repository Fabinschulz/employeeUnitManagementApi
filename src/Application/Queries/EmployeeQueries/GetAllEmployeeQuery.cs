using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries
{
    /// <summary>
    /// Represents a query to get all employees with pagination data.
    /// </summary>
    public sealed record GetAllEmployeeQuery
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total number of items.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the list of user data transfer objects.
        /// </summary>
        public List<Employee> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmployeeQuery"/> class.
        /// </summary>
        /// <param name="entity">The entity containing pagination data and user list.</param>
        public GetAllEmployeeQuery(ListDataPagination<Employee> entity)
        {
            Page = entity.Page;
            TotalPages = entity.TotalPages;
            TotalItems = entity.TotalItems;
            Data = entity.Data.Select(user => user).ToList();
        }
    }
}
