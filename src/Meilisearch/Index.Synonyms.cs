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
        /// Gets the synonyms setting.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the synonyms setting.</returns>
        public async Task<Dictionary<string, IEnumerable<string>>> GetSynonymsAsync(
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await _http.GetFromJsonAsync<Dictionary<string, IEnumerable<string>>>(
                    $"indexes/{Uid}/settings/synonyms",
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the synonyms setting.
        /// </summary>
        /// <param name="synonyms">Collection of synonyms.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> UpdateSynonymsAsync(Dictionary<string, IEnumerable<string>> synonyms,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var responseMessage =
                await _http.PutAsJsonAsync($"indexes/{Uid}/settings/synonyms", synonyms,
                        options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            return await responseMessage.Content.ReadFromJsonAsync<TaskInfo>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Resets the synonyms setting.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> ResetSynonymsAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/settings/synonyms", cancellationToken)
                .ConfigureAwait(false);
            return await httpresponse.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
