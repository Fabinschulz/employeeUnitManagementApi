using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Queries.UnitQueries
{
    /// <summary>
    /// Represents a query to get all unit with pagination data.
    /// </summary>
    public sealed record GetAllUnitQuery
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
        /// Gets or sets the list of unit data transfer objects.
        /// </summary>
        public List<Unit> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUnitQuery"/> class.
        /// </summary>
        /// <param name="entity">The entity containing pagination data and unit list.</param>
        public GetAllUnitQuery(ListDataPagination<Unit> entity)
        {
            Page = entity.Page;
            TotalPages = entity.TotalPages;
            TotalItems = entity.TotalItems;
            Data = entity.Data.Select(u => u).ToList();
        }
    }
}
