using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch;

public partial class Index
{
    /// <summary>
    /// Gets the displayed attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the displayed attributes setting.</returns>
    public async Task<IEnumerable<string>> GetDisplayedAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<IEnumerable<string>>($"indexes/{Uid}/settings/displayed-attributes",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the displayed attributes setting.
    /// </summary>
    /// <param name="displayedAttributes">Collection of displayed attributes.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateDisplayedAttributesAsync(IEnumerable<string> displayedAttributes,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/displayed-attributes", displayedAttributes,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the displayed attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetDisplayedAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http
            .DeleteAsync($"indexes/{Uid}/settings/displayed-attributes", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the distinct attribute setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the distinct attribute setting.</returns>
    public async Task<string> GetDistinctAttributeAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<string>($"indexes/{Uid}/settings/distinct-attribute",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the distinct attribute setting.
    /// </summary>
    /// <param name="distinctAttribute">Name of distinct attribute.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateDistinctAttributeAsync(string distinctAttribute,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/distinct-attribute", distinctAttribute,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the distinct attribute setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task Uid of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetDistinctAttributeAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/settings/distinct-attribute", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the filterable attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the filterable attributes setting.</returns>
    public async Task<IEnumerable<string>> GetFilterableAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<IEnumerable<string>>($"indexes/{Uid}/settings/filterable-attributes",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the filterable attributes setting.
    /// </summary>
    /// <param name="filterableAttributes">Collection of filterable attributes.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateFilterableAttributesAsync(IEnumerable<string> filterableAttributes,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/filterable-attributes", filterableAttributes,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content.ReadFromJsonAsync<TaskInfo>(cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the filterable attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetFilterableAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http
            .DeleteAsync($"indexes/{Uid}/settings/filterable-attributes", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the searchable attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the searchable attributes setting.</returns>
    public async Task<IEnumerable<string>> GetSearchableAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<IEnumerable<string>>($"indexes/{Uid}/settings/searchable-attributes",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the searchable attributes setting.
    /// </summary>
    /// <param name="searchableAttributes">Collection of searchable attributes.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateSearchableAttributesAsync(IEnumerable<string> searchableAttributes,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/searchable-attributes", searchableAttributes,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the searchable attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetSearchableAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http
            .DeleteAsync($"indexes/{Uid}/settings/searchable-attributes", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the sortable attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the sortable attributes setting.</returns>
    public async Task<IEnumerable<string>> GetSortableAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<IEnumerable<string>>($"indexes/{Uid}/settings/sortable-attributes",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the sortable attributes setting.
    /// </summary>
    /// <param name="sortableAttributes">Collection of sortable attributes.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateSortableAttributesAsync(IEnumerable<string> sortableAttributes,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/sortable-attributes", sortableAttributes,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the sortable attributes setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetSortableAttributesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/settings/sortable-attributes", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
