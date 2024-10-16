using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// Wrapper for index stats.
/// </summary>
public class IndexStats
{
    /// <summary>
    /// See <see cref="IndexStats" />.
    /// </summary>
    /// <param name="numberOfDocuments">Total number of documents in an index.</param>
    /// <param name="isIndexing">If true, the index is still processing documents and attempts to search will
    /// result in undefined behavior. If false, the index has finished processing and you can start searching.</param>
    /// <param name="fieldDistribution">Shows every field in the index along with the total number of documents
    /// containing that field in said index.</param>
    public IndexStats(int numberOfDocuments, bool isIndexing, IReadOnlyDictionary<string, int> fieldDistribution)
    {
        NumberOfDocuments = numberOfDocuments;
        IsIndexing = isIndexing;
        FieldDistribution = fieldDistribution;
    }

    /// <summary>
    /// Gets the total number of documents.
    /// </summary>
    [JsonPropertyName("numberOfDocuments")]
    public int NumberOfDocuments { get; }

    /// <summary>
    /// Gets a value indicating whether the index is currently indexing.
    /// </summary>
    [JsonPropertyName("isIndexing")]
    public bool IsIndexing { get; }

    /// <summary>
    /// Gets field distribution.
    /// </summary>
    [JsonPropertyName("fieldDistribution")]
    public IReadOnlyDictionary<string, int> FieldDistribution { get; }
}
