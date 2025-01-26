﻿using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using EmployeeUnitManagementApi.src.Infra.Services.TokenServices;
using EmployeeUnitManagementApi.src.Domain.Entities;
using EmployeeUnitManagementApi.src.Infra.Persistence;
using EmployeeUnitManagementApi.src.Infra.Services.PasswordService;
using EmployeeUnitManagementApi.src.Domain.Enums;
using EmployeeUnitManagementApi.src.Application.Common.Exceptions;
using EmployeeUnitManagementApi.src.Application.Common.Models;

namespace EmployeeUnitManagementApi.src.Infra.Repositories
{
    /// <summary>
    /// Repository class for managing user data.
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Changes the password for the user with the specified email and current password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The current password of the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>The user with the updated password.</returns>
        public async Task<User> ChangePassword(string email, string password, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            user.Password = newPassword;
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Resets the password for the user with the specified email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user with the reset password.</returns>
        public async Task<User> ForgotPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            user.Password = "";
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Gets the authenticated user based on the provided claims principal.
        /// </summary>
        /// <param name="user">The claims principal containing the user's claims.</param>
        /// <returns>The authenticated user.</returns>
        public async Task<User> GetAuthenticatedUser(ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var password = user.FindFirst(ClaimTypes.Hash)?.Value;
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (userEntity == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            return userEntity;
        }

        /// <summary>
        /// Logs in a user with the specified email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The logged-in user with a generated token.</returns>
        public async Task<User> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user == null)
            {
                throw new BadRequestException(new[] { "Email não cadastrado." });
            }

            var hashedPassword = PasswordService.HashPassword(password);
            ValidateUserForLogin(user, hashedPassword);

            var token = TokenService.GenerateToken(user);
            return CreateLoggedUser(user, token);
        }


        private static void ValidateUserForLogin(User user, string password)
        {
            if (!PasswordService.VerifyPasswordHash(password, user.Password))
                throw new BadRequestException(new[] { "Senha incorreta. Verifique suas credenciais e tente novamente." });
        }

        private User CreateLoggedUser(User user, string token)
        {
            var loggedUser = new User(user.Email, user.Password)
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                Token = token
            };
            return loggedUser;
        }

        private async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to register.</param>
        public async Task<User> Register(User user)
        {
            var existingUser = await GetUserByEmail(user.Email);

            if (existingUser != null)
            {
                throw new BadRequestException(new[] { "Usuário já existe com este email." });
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private IQueryable<User> BuildBaseQuery()
        {
            return _context.Set<User>().AsQueryable();
        }

        /// <summary>
        /// Gets a paginated list of users based on the specified filters and sorting options.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="username">The username to filter by.</param>
        /// <param name="email">The email to filter by.</param>
        /// <param name="orderBy">The field to order by.</param>
        /// <param name="status">The status to filter by.</param>
        /// <param name="role">The role to filter by.</param>
        /// <returns>A paginated list of users.</returns>
        public async Task<ListDataPagination<User>> GetAll(int page, int size, string? username, string? email, string? orderBy, StatusEnum? status, RoleEnum? role)
        {
            var query = BuildBaseQuery();

            ApplyUsernameFilter(ref query, username);
            ApplyEmailFilter(ref query, email);
            ApplyStatusFilter(ref query, status);
            ApplyRoleFilter(ref query, role);

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = ApplyOrderBy(query, orderBy);
            }

            var totalItems = await query.CountAsync();

            var data = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new ListDataPagination<User>(data, page, size, totalItems);
        }

        private static void ApplyUsernameFilter(ref IQueryable<User> query, string? username)
        {
            username = username?.ToLower().Trim();
            ApplyFilterIfNotEmpty(username, x => EF.Property<string>(x, "Username").ToLower().Contains(username!), ref query);
        }

        private static void ApplyEmailFilter(ref IQueryable<User> query, string? email)
        {
            ApplyFilterIfNotEmpty(email, x => EF.Property<string>(x, "Email") != null && EF.Property<string>(x, "Email").Contains(email!), ref query);
        }

        private static void ApplyStatusFilter(ref IQueryable<User> query, StatusEnum? status)
        {
            if (status.HasValue)
            {
                ApplyFilterIfTrue(status == StatusEnum.Ativo, x => x.Status == StatusEnum.Ativo, x => x.Status == StatusEnum.Inativo, ref query);
            }
        }

        private static void ApplyRoleFilter(ref IQueryable<User> query, RoleEnum? role)
        {
            if (role.HasValue)
            {
                var roleValue = role.Value;
                query = query.Where(x => x.Role == roleValue);
            }
        }

        private static void ApplyFilterIfNotEmpty(string? value, Expression<Func<User, bool>> filter, ref IQueryable<User> query)
        {
            if (!string.IsNullOrEmpty(value))
            {
                query = query.Where(filter);
            }
        }

        private static void ApplyFilterIfTrue(bool condition, Expression<Func<User, bool>> filterTrue, Expression<Func<User, bool>> filterFalse, ref IQueryable<User> query)
        {
            query = query.Where(condition ? filterTrue : filterFalse);
        }

        private static IQueryable<User> ApplyOrderBy(IQueryable<User> query, string orderBy)
        {
            switch (orderBy)
            {
                case "active_ASC":
                    return query.OrderBy(x => x.Status);
                case "active_DESC":
                    return query.OrderByDescending(x => x.Status);
                case "inactive_ASC":
                    return query.OrderBy(x => x.Status);
                case "inactive_DESC":
                    return query.OrderByDescending(x => x.Status);
                case "createdAt_ASC":
                    return query.OrderBy(x => x.CreatedAt);
                case "createdAt_DESC":
                    return query.OrderByDescending(x => x.CreatedAt);
                case "username_ASC":
                    return query.OrderBy(x => x.Username);
                case "username_DESC":
                    return query.OrderByDescending(x => x.Username);
                case "email_ASC":
                    return query.OrderBy(x => x.Email);
                case "email_DESC":
                    return query.OrderByDescending(x => x.Email);
                case "role_ASC":
                    return query.OrderBy(x => x.Role);
                case "role_DESC":
                    return query.OrderByDescending(x => x.Role);
                default:
                    return query.OrderByDescending(x => x.CreatedAt);
            }
        }
    }
}
