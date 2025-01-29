using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using EmployeeUnitManagementApi.src.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeUnitManagementApi.src.Infra.Repositories
{
    /// <summary>
    /// Repository class for managing Employee entities.
    /// </summary>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        private IQueryable<Employee> BuildBaseQuery()
        {
            return _context.Set<Employee>().AsQueryable();
        }
        /// <summary>
        /// Retrieves a paginated list of employees with optional filtering and sorting.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="name">The name filter.</param>
        /// <param name="orderBy">The order by clause.</param>
        /// <returns>A paginated list of employees.</returns>
        public async Task<ListDataPagination<Employee>> GetAll(int page, int size, string? name, string? orderBy)
        {
            var query = BuildBaseQuery();
            ApplyNameFilter(ref query, name);

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = ApplyOrderBy(query, orderBy);
            }

            var totalItems = await query.CountAsync();

            var data = await query.Skip(page * size).Take(size).ToListAsync();

            return new ListDataPagination<Employee>(data, page, size, totalItems);
        }

        private static void ApplyNameFilter(ref IQueryable<Employee> query, string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(name.ToLower()));
            }
        }

        private static IQueryable<Employee> ApplyOrderBy(IQueryable<Employee> query, string orderBy)
        {
            switch (orderBy)
            {
                case "createdAt_ASC":
                    return query.OrderBy(x => x.CreatedAt);
                case "createdAt_DESC":
                    return query.OrderByDescending(x => x.CreatedAt);
                case "name_ASC":
                    return query.OrderBy(x => x.Name);
                case "name_DESC":
                    return query.OrderByDescending(x => x.Name);
                case "unit_ASC":
                    return query.OrderBy(x => x.Unit);
                case "unit_DESC":
                    return query.OrderByDescending(x => x.Unit);
                default:
                    return query.OrderByDescending(x => x.CreatedAt);
            }
        }
    }
}
