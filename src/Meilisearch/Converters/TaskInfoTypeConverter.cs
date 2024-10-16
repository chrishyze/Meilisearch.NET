using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Meilisearch.Converters;

/// <summary>
/// JSON converter for <see cref="TaskInfoType">TaskInfoType</see>
/// </summary>
public class TaskInfoTypeConverter : JsonConverter<TaskInfoType>
{
    /// <summary>
    /// Reads and converts the JSON to type TaskInfoType.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The options to use during deserialization.</param>
    /// <returns></returns>
    public override TaskInfoType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            return TaskInfoType.Unknown;
        }

        var enumValue = reader.GetString();
        return Enum.TryParse<TaskInfoType>(enumValue, true, out var taskInfoType) ? taskInfoType :
            // If we reach here, it means we encountered an unknown value
            TaskInfoType.Unknown;
    }

    /// <summary>
    /// Writes TaskInfoType as JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The TaskInfoType value.</param>
    /// <param name="options">The options to use during serialization.</param>
    /// <returns></returns>
    public override void Write(Utf8JsonWriter writer, TaskInfoType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
