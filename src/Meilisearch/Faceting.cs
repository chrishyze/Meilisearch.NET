using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// Faceting configuration.
/// </summary>
public class Faceting
{
    /// <summary>
    /// The maximum number of values to return for each facet.
    /// </summary>
    [JsonPropertyName("maxValuesPerFacet")]
    public int MaxValuesPerFacet { get; set; }
}
