using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch;

public partial class Index
{
    /// <summary>
    /// Gets the stop words setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the stop words setting.</returns>
    public async Task<IEnumerable<string>> GetStopWordsAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<IEnumerable<string>>($"indexes/{Uid}/settings/stop-words",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the stop words setting.
    /// </summary>
    /// <param name="stopWords">Collection of stop words.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateStopWordsAsync(IEnumerable<string> stopWords,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/stop-words", stopWords,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content.ReadFromJsonAsync<TaskInfo>(cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the stop words setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetStopWordsAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/settings/stop-words", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
