namespace Meilisearch;

/// <summary>
/// Generic result class for resources.
/// When returning a list, Meilisearch stores the data in the "results" field, to allow better pagination.
/// </summary>
/// <typeparam name="T">Type of the Meilisearch server object. Ex: keys, indexes, ...</typeparam>
public class TasksResults<T> : Result<T>
{
    /// <summary>
    /// See <see cref="TasksResults{T}" />.
    /// </summary>
    /// <param name="results">Task objects.</param>
    /// <param name="limit">Number of tasks returned.</param>
    /// <param name="from">uid of the first task returned.</param>
    /// <param name="next">Value passed to from to view the next "page" of results.
    /// When the value of next is null, there are no more tasks to view.</param>
    /// <param name="total">Total number of tasks matching the filter or query.</param>
    public TasksResults(T results, int? limit, int? from, int? next, int? total)
        : base(results, limit)
    {
        From = from;
        Next = next;
        Total = total;
    }

    /// <summary>
    /// Gets from size.
    /// </summary>
    public int? From { get; }

    /// <summary>
    /// Gets next size.
    /// </summary>
    public int? Next { get; }

    /// <summary>
    /// Gets total number of tasks.
    /// </summary>
    public int? Total { get; }
}
