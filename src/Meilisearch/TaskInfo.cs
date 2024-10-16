using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Meilisearch.Converters;

namespace Meilisearch;

/// <summary>
/// Information of the regarding a task.
/// </summary>
public class TaskInfo
{
    /// <summary>
    /// See <see cref="TaskInfo"/>.
    /// </summary>
    /// <param name="taskUid">Unique sequential identifier of the task.</param>
    /// <param name="indexUid">Unique identifier of the targeted index.</param>
    /// <param name="status">Status of the task.</param>
    /// <param name="type">Type of operation performed by the task.</param>
    /// <param name="details">Detailed information on the task payload. This object's contents depend on the task's type.</param>
    /// <param name="error">If the task has the failed status, then this object contains the error definition.</param>
    /// <param name="duration">The total elapsed time the task spent in the processing state.</param>
    /// <param name="enqueuedAt">The date and time when the task was first enqueued.</param>
    /// <param name="startedAt">The date and time when the task began processing</param>
    /// <param name="finishedAt">The date and time when the task finished.</param>
    public TaskInfo(int taskUid, string indexUid, TaskInfoStatus status, TaskInfoType type,
        IReadOnlyDictionary<string, object> details, IReadOnlyDictionary<string, string> error, string duration, DateTime enqueuedAt,
        DateTime? startedAt, DateTime? finishedAt)
    {
        TaskUid = taskUid;
        IndexUid = indexUid;
        Status = status;
        Type = type;
        Details = details;
        Error = error;
        Duration = duration;
        EnqueuedAt = enqueuedAt;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }

    /// <summary>
    /// The unique sequential identifier of the task.
    /// </summary>
    [JsonPropertyName("taskUid")]
    public int TaskUid { get; }

    /// <summary>
    /// The unique index identifier.
    /// </summary>
    [JsonPropertyName("indexUid")]
    public string IndexUid { get; }

    /// <summary>
    /// The status of the task. Possible values are enqueued, processing, succeeded, failed.
    /// </summary>
    [JsonPropertyName("status")]
    public TaskInfoStatus Status { get; }

    /// <summary>
    /// The type of task.
    /// </summary>
    [JsonPropertyName("type")]
    public TaskInfoType Type { get; }

    /// <summary>
    /// Detailed information on the task payload.
    /// </summary>
    [JsonPropertyName("details")]
    public IReadOnlyDictionary<string, object> Details { get; }

    /// <summary>
    /// Error details and context. Only present when a task has the failed status.
    /// </summary>
    [JsonPropertyName("error")]
    public IReadOnlyDictionary<string, string> Error { get; }

    /// <summary>
    /// The total elapsed time the task spent in the processing state, in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("duration")]
    public string Duration { get; }

    /// <summary>
    /// The date and time when the task was first enqueued, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("enqueuedAt")]
    public DateTime EnqueuedAt { get; }

    /// <summary>
    /// The date and time when the task began processing, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; }

    /// <summary>
    /// The date and time when the task finished processing, whether failed or succeeded, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("finishedAt")]
    public DateTime? FinishedAt { get; }

    /// <summary>
    /// A taskUid who canceled the current task.
    /// </summary>
    [JsonPropertyName("canceledBy")]
    public int? CanceledBy { get; }
}

/// <summary>
/// Status of the task.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskInfoStatus
{
    /// <summary>
    /// The task is enqueued.
    /// </summary>
    Enqueued,
    /// <summary>
    /// The task is processing.
    /// </summary>
    Processing,
    /// <summary>
    /// The task has completed successfully.
    /// </summary>
    Succeeded,
    /// <summary>
    /// The task is failed.
    /// </summary>
    Failed,
    /// <summary>
    /// The task is canceled.
    /// </summary>
    Canceled
}

/// <summary>
/// Type of operation performed by the task.
/// </summary>
[JsonConverter(typeof(TaskInfoTypeConverter))]
public enum TaskInfoType
{
    /// <summary>
    /// indexCreation tasks.
    /// </summary>
    IndexCreation,
    /// <summary>
    /// indexUpdate tasks.
    /// </summary>
    IndexUpdate,
    /// <summary>
    /// indexDeletion tasks.
    /// </summary>
    IndexDeletion,
    /// <summary>
    /// documentAdditionOrUpdate tasks.
    /// </summary>
    DocumentAdditionOrUpdate,
    /// <summary>
    /// documentDeletion tasks.
    /// </summary>
    DocumentDeletion,
    /// <summary>
    /// settingsUpdate tasks.
    /// </summary>
    SettingsUpdate,
    /// <summary>
    /// dumpCreation task.
    /// </summary>
    DumpCreation,
    /// <summary>
    /// taskCancelation tasks.
    /// </summary>
    TaskCancelation,
    /// <summary>
    /// snapshotCreation tasks.
    /// </summary>
    SnapshotCreation,
    /// <summary>
    /// taskDeletion tasks.
    /// </summary>
    TaskDeletion,
    /// <summary>
    /// indexSwap tasks.
    /// </summary>
    IndexSwap,
    /// <summary>
    /// Unknown tasks.
    /// </summary>
    Unknown
}
