using EmployeeUnitManagementApi.src.Application.Queries;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UserCommand
{
    /// <summary>
    /// Command to get a user by Id.
    /// </summary>
    /// <param name="Id">The Id of the user.</param>
    public sealed record GetUserByIdCommand(Guid Id) : IRequest<GetUserByIdQuery>;

    /// <summary>
    /// Validator for the GetUserByIdCommand.
    /// </summary>
    public sealed class GetUserByIdValidator : AbstractValidator<GetUserByIdCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdValidator"/> class.
        /// </summary>
        public GetUserByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .Must(id => IsGuidValid(id)).WithMessage("Id is not a valid GUID");
        }

        private bool IsGuidValid(Guid id)
        {
            return Guid.TryParse(id.ToString(), out _);
        }
    }
}
