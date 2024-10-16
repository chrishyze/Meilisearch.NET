using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch
{
    public partial class Index
    {
        /// <summary>
        /// Gets all the separator tokens settings.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns all the configured separator tokens.</returns>
        public async Task<List<string>> GetSeparatorTokensAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await _http.GetFromJsonAsync<List<string>>($"indexes/{Uid}/settings/separator-tokens",
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates all the separator tokens settings.
        /// </summary>
        /// <param name="separatorTokens">Collection of separator tokens.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> UpdateSeparatorTokensAsync(IEnumerable<string> separatorTokens,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var responseMessage =
                await _http.PutAsJsonAsync($"indexes/{Uid}/settings/separator-tokens", separatorTokens,
                        options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Resets all the separator tokens settings.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> ResetSeparatorTokensAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/settings/separator-tokens", cancellationToken)
                .ConfigureAwait(false);
            return await httpresponse.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
