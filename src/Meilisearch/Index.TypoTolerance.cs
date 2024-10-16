using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch
{
    public partial class Index
    {
        /// <summary>
        /// Gets the typo tolerance setting.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the typo tolerance setting.</returns>
        public async Task<TypoTolerance> GetTypoToleranceAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await _http.GetFromJsonAsync<TypoTolerance>($"indexes/{Uid}/settings/typo-tolerance",
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the typo tolerance setting.
        /// </summary>
        /// <param name="typoTolerance">TypoTolerance instance</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> UpdateTypoToleranceAsync(TypoTolerance typoTolerance,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var responseMessage =
                await _http.PatchAsJsonAsync($"indexes/{Uid}/settings/typo-tolerance", typoTolerance,
                        options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Resets the typo tolerance setting.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> ResetTypoToleranceAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var response = await _http.DeleteAsync($"indexes/{Uid}/settings/typo-tolerance", cancellationToken)
                .ConfigureAwait(false);

            return await response.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
