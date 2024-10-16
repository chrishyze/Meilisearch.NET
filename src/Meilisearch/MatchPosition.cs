using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// The location of each occurrence of queried terms across all fields.
/// </summary>
public class MatchPosition
{
    /// <summary>
    /// See <see cref="MatchPosition" />.
    /// </summary>
    /// <param name="start">The start of the matching term.</param>
    /// <param name="length">The length of the matching term.</param>
    public MatchPosition(int start, int length)
    {
        Start = start;
        Length = length;
    }

    /// <summary>
    /// The beginning of a matching term within a field.
    /// WARNING: This value is in bytes and not the number of characters. For example, ü represents two bytes but one character.
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; }

    /// <summary>
    /// The length of a matching term within a field.
    /// WARNING: This value is in bytes and not the number of characters. For example, ü represents two bytes but one character.
    /// </summary>
    [JsonPropertyName("length")]
    public int Length { get; }
}
