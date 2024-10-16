using System.Text.Json.Serialization;

namespace Meilisearch.QueryParameters;

/// <summary>
/// A class that handles the creation of a query string when deleting documents.
/// </summary>
public class DeleteDocumentsQuery
{
    /// <summary>
    /// The filter of the documents.
    /// </summary>
    [JsonPropertyName("filter")]
    public object Filter { get; set; }
}
