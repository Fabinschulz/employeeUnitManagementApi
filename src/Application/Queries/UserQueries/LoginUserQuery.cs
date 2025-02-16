﻿namespace EmployeeUnitManagementApi.src.Application.Queries.UserQueries
{
    /// <summary>
    /// Represents a query for logging in a user.
    /// </summary>
    public sealed record LoginUserQuery
    {
        /// <summary>
        /// Gets the ID of the user.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Gets the token of the user.
        /// </summary>
        public string Token { get; init; } = string.Empty;
    }
}
