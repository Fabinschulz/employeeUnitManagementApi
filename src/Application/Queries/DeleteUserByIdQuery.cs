namespace EmployeeUnitManagementApi.src.Application.Queries
{
    /// <summary>
    /// Represents a query to delete a user by ID.
    /// </summary>
    public sealed class DeleteUserByIdQuery
    {
        /// <summary>
        /// Gets a value indicating whether the deletion was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a message associated with the deletion result.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserByIdQuery"/> class.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the deletion was successful.</param>
        /// <param name="message">A message associated with the deletion result.</param>
        public DeleteUserByIdQuery(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
