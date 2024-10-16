using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// Model for index swaps requests.
/// </summary>
public class IndexSwap
{
    /// <summary>
    /// Array of the two indexUids to be swapped.
    /// </summary>
    [JsonPropertyName("indexes")]
    public List<string> Indexes { get; private set; }

    /// <summary>
    /// Swap indexes.
    /// </summary>
    /// <param name="indexA">The indexA.</param>
    /// <param name="indexB">The indexB.</param>
    public IndexSwap(string indexA, string indexB)
    {
        this.Indexes = new List<string> { indexA, indexB };
    }
}
