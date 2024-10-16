using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch;

public partial class Index
{
    /// <summary>
    /// Gets the pagination setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the pagination setting.</returns>
    public async Task<Pagination> GetPaginationAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<Pagination>($"indexes/{Uid}/settings/pagination",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the pagination setting.
    /// </summary>
    /// <param name="pagination">Pagination instance</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> UpdatePaginationAsync(Pagination pagination,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var responseMessage =
            await _http.PatchAsJsonAsync($"indexes/{Uid}/settings/pagination", pagination,
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return await responseMessage.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Resets the pagination setting.
    /// </summary>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of the asynchronous task.</returns>
    public async Task<TaskInfo> ResetPaginationAsync(JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var response = await _http.DeleteAsync($"indexes/{Uid}/settings/pagination", cancellationToken)
            .ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
