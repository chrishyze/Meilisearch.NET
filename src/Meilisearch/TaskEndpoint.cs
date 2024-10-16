using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Meilisearch.Errors;
using Meilisearch.Extensions;
using Meilisearch.QueryParameters;

namespace Meilisearch;

/// <summary>
/// Meilisearch index to search and manage documents.
/// </summary>
public class TaskEndpoint
{
    private HttpClient _http;

    /// <summary>
    /// Gets the tasks.
    /// </summary>
    /// <param name="query">Query parameters supports by the method.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns a list of the tasks.</returns>
    public async Task<TasksResults<IEnumerable<TaskResource>>> GetTasksAsync(TasksQuery query = default,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var uri = query.ToQueryString(uri: "tasks");
        return await _http.GetFromJsonAsync<TasksResults<IEnumerable<TaskResource>>>(uri,
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel tasks given a specific query.
    /// </summary>
    /// <param name="query">Query parameters supports by the method.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns a list of the tasks.</returns>
    public async Task<TaskInfo> CancelTasksAsync(CancelTasksQuery query,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var uri = query.ToQueryString(uri: "tasks/cancel");

        var response = await _http.PostAsync(uri, null, cancellationToken: cancellationToken).ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete tasks given a specific query.
    /// </summary>
    /// <param name="query">Query parameters supports by the method.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns a list of the tasks.</returns>
    public async Task<TaskInfo> DeleteTasksAsync(DeleteTasksQuery query,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var uri = query.ToQueryString(uri: "tasks");

        var response = await _http.DeleteAsync(uri, cancellationToken).ConfigureAwait(false);

        return await response.Content.ReadFromJsonAsync<TaskInfo>(
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets one task.
    /// </summary>
    /// <param name="taskUid">Uid of the index.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task.</returns>
    public async Task<TaskResource> GetTaskAsync(int taskUid,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<TaskResource>($"tasks/{taskUid}", options: options,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the tasks from an index.
    /// </summary>
    /// <param name="indexUid">Uid of the index.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns a list of tasks of an index.</returns>
    public async Task<TasksResults<IEnumerable<TaskResource>>> GetIndexTasksAsync(string indexUid,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await _http.GetFromJsonAsync<TasksResults<IEnumerable<TaskResource>>>($"tasks?indexUid={indexUid}",
                options: options, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Waits until the asynchronous task was done.
    /// </summary>
    /// <param name="taskUid">Unique identifier of the asynchronous task.</param>
    /// <param name="timeoutMs">Timeout in millisecond.</param>
    /// <param name="intervalMs">Interval in millisecond between each check.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the task info of finished task.</returns>
    public async Task<TaskResource> WaitForTaskAsync(
        int taskUid,
        double timeoutMs = 5000.0,
        int intervalMs = 50,
        JsonSerializerOptions options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        var endingTime = DateTime.Now.AddMilliseconds(timeoutMs);

        while (DateTime.Now < endingTime)
        {
            var task = await GetTaskAsync(taskUid, options, cancellationToken).ConfigureAwait(false);

            if (task.Status != TaskInfoStatus.Enqueued && task.Status != TaskInfoStatus.Processing)
            {
                return task;
            }

            await Task.Delay(intervalMs, cancellationToken).ConfigureAwait(false);
        }

        throw new MeilisearchTimeoutError("The task " + taskUid + " timed out.");
    }

    /// <summary>
    /// Initializes the Index with HTTP client. Only for internal usage.
    /// </summary>
    /// <param name="http">HttpRequest instance used.</param>
    /// <returns>The same object with the initialization.</returns>
    // internal Index WithHttpClient(HttpClient client)
    internal TaskEndpoint WithHttpClient(HttpClient http)
    {
        _http = http;
        return this;
    }
}
