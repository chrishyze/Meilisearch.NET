using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// Generic result class.
/// When returning a list, Meilisearch stores the data in the "results" field, to allow better pagination.
/// </summary>
/// <typeparam name="T">Type of the Meilisearch server object. Ex: keys, tasks, ...</typeparam>
public class Result<T>
{
    /// <summary>
    /// See <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="results">Result objects.</param>
    /// <param name="limit">The limitation number of per-page.</param>
    public Result(T results, int? limit)
    {
        Results = results;
        Limit = limit;
    }

    /// <summary>
    /// Gets the "results" field.
    /// </summary>
    [JsonPropertyName("results")]
    public T Results { get; }

    /// <summary>
    /// Gets limit size.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; }
}
