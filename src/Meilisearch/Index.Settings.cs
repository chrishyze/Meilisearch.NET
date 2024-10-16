using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meilisearch
{
    public partial class Index
    {
        /// <summary>
        /// Gets all the settings of an index.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns all the settings.</returns>
        public async Task<Settings> GetSettingsAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await _http.GetFromJsonAsync<Settings>($"indexes/{Uid}/settings",
                    options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates all the settings of an index.
        /// The settings that are not passed in parameter are not overwritten.
        /// </summary>
        /// <param name="settings">Settings object.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> UpdateSettingsAsync(Settings settings,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var responseMessage =
                await _http.PatchAsJsonAsync($"indexes/{Uid}/settings", settings,
                        options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Resets all the settings to their default values.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info of the asynchronous task.</returns>
        public async Task<TaskInfo> ResetSettingsAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var httpResponse =
                await _http.DeleteAsync($"indexes/{Uid}/settings", cancellationToken).ConfigureAwait(false);
            return await httpResponse.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
