using EmployeeUnitManagementApi.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeUnitManagementApi.src.Domain.Interfaces
{
    /// <summary>
    /// Represents the application database context interface.
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Gets the Users DbSet.
        /// </summary>
        DbSet<User> Users { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
