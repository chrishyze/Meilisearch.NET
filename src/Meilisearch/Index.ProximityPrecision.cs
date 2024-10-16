using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch
{
    public partial class Index
    {
        /// <summary>
        /// Gets the proximity precision setting.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the proximity precision setting.</returns>
        public async Task<string> GetProximityPrecisionAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await _http.GetFromJsonAsync<string>($"indexes/{Uid}/settings/proximity-precision",
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the proximity precision setting.
        /// </summary>
        /// <param name="proximityPrecision">The new proximity precision setting.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> UpdateProximityPrecisionAsync(string proximityPrecision,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var responseMessage =
                await _http.PutAsJsonAsync($"indexes/{Uid}/settings/proximity-precision", proximityPrecision,
                        options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Resets proximity precision setting.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> ResetProximityPrecisionAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var response = await _http.DeleteAsync($"indexes/{Uid}/settings/proximity-precision", cancellationToken)
                .ConfigureAwait(false);

            return await response.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
