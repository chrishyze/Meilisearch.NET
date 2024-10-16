using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// Wrapper for index stats.
/// </summary>
public class Stats
{
    /// <summary>
    /// See <see cref="Stats" />.
    /// </summary>
    /// <param name="databaseSize">Size of the database in bytes.</param>
    /// <param name="lastUpdate">When the last update was made to the database.</param>
    /// <param name="indexes">Object containing the statistics for each index found in the database.</param>
    public Stats(long databaseSize, DateTime? lastUpdate, IReadOnlyDictionary<string, IndexStats> indexes)
    {
        DatabaseSize = databaseSize;
        LastUpdate = lastUpdate;
        Indexes = indexes;
    }

    /// <summary>
    /// Gets database size.
    /// </summary>
    [JsonPropertyName("databaseSize")]
    public long DatabaseSize { get; }

    /// <summary>
    /// Gets last update timestamp.
    /// </summary>
    [JsonPropertyName("lastUpdate")]
    public DateTime? LastUpdate { get; }

    /// <summary>
    /// Gets index stats.
    /// </summary>
    [JsonPropertyName("indexes")]
    public IReadOnlyDictionary<string, IndexStats> Indexes { get; }
}
