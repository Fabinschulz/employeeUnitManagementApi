using EmployeeUnitManagementApi.src.Application.Common.Models;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using EmployeeUnitManagementApi.src.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeUnitManagementApi.src.Infra.Repositories
{
    /// <summary>
    /// Repository for managing unit entities.
    /// </summary>
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="UnitRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitRepository(AppDbContext context) : base(context)
        {
        }

        private IQueryable<Unit> BuildBaseQuery()
        {
            return _context.Set<Unit>().AsQueryable();
        }

        /// <summary>
        /// Retrieves a paginated list of unit with optional filtering and sorting.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="name">The name filter.</param>
        /// <param name="orderBy">The order by clause.</param>
        /// <returns>A paginated list of unit.</returns>
        public async Task<ListDataPagination<Unit>> GetAll(int page, int size, string? name, string? orderBy)
        {
            var query = BuildBaseQuery();
            ApplyNameFilter(ref query, name);

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = ApplyOrderBy(query, orderBy);
            }

            var totalItems = await query.CountAsync();

            var data = await query.Skip(page * size).Take(size).ToListAsync();

            return new ListDataPagination<Unit>(data, page, size, totalItems);
        }

        private static void ApplyNameFilter(ref IQueryable<Unit> query, string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(name.ToLower()));
            }
        }

        private static IQueryable<Unit> ApplyOrderBy(IQueryable<Unit> query, string orderBy)
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
                    return query.OrderBy(x => x.Employees);
                case "unit_DESC":
                    return query.OrderByDescending(x => x.Employees);
                default:
                    return query.OrderByDescending(x => x.CreatedAt);
            }
        }
    }
}
