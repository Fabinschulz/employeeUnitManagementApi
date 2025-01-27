using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeUnitManagementApi.src.Domain.Enums
{
    /// <summary>
    /// Represents the status of a user.
    /// </summary>
    public enum StatusEnum
    {

        /// <summary>
        /// Represents an active user.
        /// </summary>
        [Description("Usuário ativo")]
        Ativo,

        /// <summary>
        /// Represents an inactive user.
        /// </summary>
        [Description("Usuário inativo")]
        Inativo
    }

    /// <summary>
    /// Converts a StatusEnum to and from JSON.
    /// </summary>
    public class StatusEnumConverter : JsonConverter<StatusEnum>
    {
        /// <summary>
        /// Reads and converts the JSON to a StatusEnum.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">The serializer options.</param>
        /// <returns>The converted StatusEnum.</returns>
        public override StatusEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Verifica se o valor é numérico
            if (reader.TokenType == JsonTokenType.Number)
            {
                var intValue = reader.GetInt32();
                if (Enum.IsDefined(typeof(StatusEnum), intValue))
                {
                    return (StatusEnum)intValue;
                }

                throw new JsonException($"Invalid numeric value for {nameof(StatusEnum)}: {intValue}");
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (Enum.TryParse<StatusEnum>(value, true, out var status))
                {
                    return status;
                }

                throw new JsonException($"Invalid string value for {nameof(StatusEnum)}: {value}");
            }

            throw new JsonException($"Unexpected token type for {nameof(StatusEnum)}: {reader.TokenType}");
        }


        /// <summary>
        /// Writes and converts the StatusEnum to JSON.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The StatusEnum value to convert.</param>
        /// <param name="options">The serializer options.</param>
        public override void Write(Utf8JsonWriter writer, StatusEnum value, JsonSerializerOptions options)
        {
            // Serializa usando o valor do enum como string
            writer.WriteStringValue(value.ToString());
        }
    }
}
