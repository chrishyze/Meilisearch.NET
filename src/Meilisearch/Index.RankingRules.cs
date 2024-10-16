using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch;

public partial class Index
{
    /// <summary>
    /// Gets the ranking rules setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the ranking rules setting.</returns>
    public async Task<IEnumerable<string>> GetRankingRulesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<IEnumerable<string>>($"indexes/{Uid}/settings/ranking-rules",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the ranking rules setting.
    /// </summary>
    /// <param name="rankingRules">Collection of ranking rules.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdateRankingRulesAsync(IEnumerable<string> rankingRules,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PutAsJsonAsync($"indexes/{Uid}/settings/ranking-rules", rankingRules,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the ranking rules setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetRankingRulesAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/settings/ranking-rules", cancellationToken)
            .ConfigureAwait(false);
        return await httpresponse.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
