using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Meilisearch.QueryParameters;

namespace Meilisearch;

public partial class Index
{
    /// <summary>
    /// Gets the tasks.
    /// </summary>
    /// <param name="query">Query parameters supports by the method.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns a list of the operations status.</returns>
    public async Task<TasksResults<IEnumerable<TaskResource>>> GetTasksAsync(TasksQuery query = null,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        query ??= new TasksQuery { IndexUids = new List<string> { this.Uid } };

        return await TaskEndpoint().GetTasksAsync(query, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get on task.
    /// </summary>
    /// <param name="taskUid">Uid of the task.</param>
    /// <param name="options">The JSON serialization options.</param>
    /// <param name="cancellationToken">The cancellation token for this call.</param>
    /// <returns>Returns the tasks.</returns>
    public async Task<TaskResource> GetTaskAsync(int taskUid,
        JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        options ??= Constants.JsonSerializerOptionsRemoveNulls;
        return await TaskEndpoint().GetTaskAsync(taskUid, options, cancellationToken).ConfigureAwait(false);
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
        return await TaskEndpoint().WaitForTaskAsync(taskUid, timeoutMs, intervalMs, options, cancellationToken)
            .ConfigureAwait(false);
    }
}
