using System;
using System.Collections.Generic;
using System.Linq;

namespace Meilisearch.Extensions;

/// <summary>
/// Extensions methods for IEnumerable.
/// </summary>
internal static class EnumerableExtensions
{
    /// <summary>
    /// Returns chunks of a list.
    /// </summary>
    /// <param name="fullList">The list to split.</param>
    /// <param name="chunkSize">Size of the chunks.</param>
    /// <typeparam name="T">Type of objects in the list.</typeparam>
    /// <returns>List of chunks.</returns>
    /// <exception cref="ArgumentNullException">Thrown if fullList is null.</exception>
    /// <exception cref="ArgumentException">Throw if chunkSize is lower than 1.</exception>
    internal static IEnumerable<IEnumerable<T>> GetChunks<T>(this IEnumerable<T> fullList, int chunkSize)
    {
        ArgumentNullException.ThrowIfNull(fullList);

        if (chunkSize < 1)
        {
            throw new ArgumentException("chunkSize value must be greater than 0", nameof(chunkSize));
        }

        var enumerable = fullList.ToList();
        var total = enumerable.Count;
        var sent = 0;
        while (sent < total)
        {
            yield return enumerable.Skip(sent).Take(chunkSize);
            sent += chunkSize;
        }
    }
}
