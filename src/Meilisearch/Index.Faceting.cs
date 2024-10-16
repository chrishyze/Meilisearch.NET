using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch;

public partial class Index
{
    /// <summary>
    /// Gets the faceting setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the faceting setting.</returns>
    public async Task<Faceting> GetFacetingAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<Faceting>($"indexes/{Uid}/settings/faceting",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the faceting setting.
    /// </summary>
    /// <param name="faceting">Faceting instance</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateFacetingAsync(Faceting faceting,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PatchAsJsonAsync($"indexes/{Uid}/settings/faceting", faceting,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the faceting setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetFacetingAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var response = await _http.DeleteAsync($"indexes/{Uid}/settings/faceting", cancellationToken)
            .ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
