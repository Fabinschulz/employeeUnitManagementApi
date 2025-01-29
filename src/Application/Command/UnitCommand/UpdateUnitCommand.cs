using System.Text.Json.Serialization;
using EmployeeUnitManagementApi.src.Application.Queries.UnitQueries;
using EmployeeUnitManagementApi.src.Domain.Enums;
using FluentValidation;
using MediatR;

namespace EmployeeUnitManagementApi.src.Application.Command.UnitCommand
{
    /// <summary>
    /// Command to update a unit.
    /// </summary>
    /// <param name="Id">The unique identifier of the unit.</param>
    /// <param name="Name">The name of the unit.</param>
    /// <param name="Status">The status of the unit.</param>
    public sealed record UpdateUnitCommand(
        Guid Id,
        string Name,
        [property: JsonConverter(typeof(StatusEnumConverter))] StatusEnum Status
        ) : IRequest<UpdateUnitQuery>;

    /// <summary>
    /// Validator for the UpdateUserCommand.
    /// </summary>
    public sealed class UpdateUnitValidator : AbstractValidator<UpdateUnitCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnitValidator"/> class.
        /// </summary>
        public UpdateUnitValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O campo 'Nome da unidade' é obrigatório.");
        }
    }
}
