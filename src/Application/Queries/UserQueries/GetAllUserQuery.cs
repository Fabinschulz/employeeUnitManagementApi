using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Application.Queries.UserQueries.DTOs;
using EmployeeUnitManagementApi.src.Domain.Entities;

namespace EmployeeUnitManagementApi.src.Application.Queries.UserQueries
{
    /// <summary>
    /// Represents a query to get all users with pagination.
    /// </summary>
    public sealed record GetAllUserQuery
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
        public List<UserDto> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUserQuery"/> class.
        /// </summary>
        /// <param name="entity">The entity containing pagination data and user list.</param>
        public GetAllUserQuery(ListDataPagination<User> entity)
        {
            Page = entity.Page;
            TotalPages = entity.TotalPages;
            TotalItems = entity.TotalItems;
            Data = entity.Data.Select(user => new UserDto(user)).ToList();
        }
    }
}
