using EmployeeUnitManagementApi.src.Domain.Common;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Domain.Enums;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeUnitManagementApi.src.Infra.Persistence
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IApplicationDbContext
    {
        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Gets or sets the Units.
        /// </summary>
        public DbSet<Unit> Units => Set<Unit>();

        /// <summary>
        /// Gets or sets the Employees.
        /// </summary>
        public DbSet<Employee> Employees => Set<Employee>();

        /// <summary>
        /// Configures the schema needed for the context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Role).IsRequired(false).HasMaxLength(50).HasColumnType("varchar(50)");
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();

            // Relacionamento User ↔ Employee (1:1)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento Unit ↔ Employee (1:N)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Unit)  // Employee tem uma Unit
                .WithMany(u => u.Employees) // Unit tem muitos Employees
                .HasForeignKey(e => e.UnitId) // FK em Employee
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Saves all changes made in this context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var dateTimeUtcNow = DateTimeOffset.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = dateTimeUtcNow;
                        entry.Entity.Status = StatusEnum.Ativo;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = dateTimeUtcNow;
                        entry.Entity.Status = StatusEnum.Ativo;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedAt = dateTimeUtcNow;
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.Status = StatusEnum.Inativo;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
