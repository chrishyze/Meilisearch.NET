using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// Generic result class for resources.
/// When returning a list, Meilisearch stores the data in the "results" field, to allow better pagination.
/// </summary>
/// <typeparam name="T">Type of the Meilisearch server object. Ex: keys, indexes, ...</typeparam>
public class ResourceResults<T> : Result<T>
{
    /// <summary>
    /// See <see cref="ResourceResults{T}" />.
    /// </summary>
    /// <param name="results">Resource objects.</param>
    /// <param name="limit">The limitation number of per-page.</param>
    /// <param name="offset">The offset of the results.</param>
    /// <param name="total">Total number of results.</param>
    public ResourceResults(T results, int? limit, int offset, int total)
        : base(results, limit)
    {
        Offset = offset;
        Total = total;
    }

    /// <summary>
    /// Gets offset size.
    /// </summary>
    [JsonPropertyName("offset")]
    public int Offset { get; }

    /// <summary>
    /// Gets total size.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; }
}
