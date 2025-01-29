namespace EmployeeUnitManagementApi.src.Application.Queries.EmployeeQueries
{
    /// <summary>
    /// Represents a query to delete an employee by ID.
    /// </summary>
    public class DeleteEmployeeByIdQuery
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
        /// Initializes a new instance of the <see cref="DeleteEmployeeByIdQuery"/> class.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the deletion was successful.</param>
        /// <param name="message">A message associated with the deletion result.</param>
        public DeleteEmployeeByIdQuery(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
