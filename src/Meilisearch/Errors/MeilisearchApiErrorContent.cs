using System.Text.Json.Serialization;

namespace Meilisearch.Errors;

/// <summary>
/// Error sent by Meilisearch API.
/// </summary>
public class MeilisearchApiErrorContent
{
    /// <summary>
    /// See <see cref="MeilisearchApiErrorContent" />.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="code">The error code.</param>
    /// <param name="type">The error type.</param>
    /// <param name="link">The error link.</param>
    public MeilisearchApiErrorContent(string message, string code, string type, string link)
    {
        Message = message;
        Code = code;
        Type = type;
        Link = link;
    }

    /// <summary>
    /// Gets the message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; }

    /// <summary>
    /// Gets the code.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; }

    /// <summary>
    /// Gets the link.
    /// </summary>
    [JsonPropertyName("link")]
    public string Link { get; }
}
