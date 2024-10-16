using System;
using System.Net;

namespace Meilisearch.Errors;

/// <summary>
/// Error sent by Meilisearch API.
/// </summary>
public class MeilisearchApiError : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeilisearchApiError"/> class.
    /// Handler Exception received from Meilisearch API.
    /// </summary>
    /// <param name="apiError">Specific error message from Meilisearch Api.</param>
    public MeilisearchApiError(MeilisearchApiErrorContent apiError)
        : base(
            $"MeilisearchApiError, Message: {apiError.Message}, Code: {apiError.Code}, Type: {apiError.Type}, Link: {apiError.Link}")
    {
        Code = apiError.Code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MeilisearchApiError"/> class.
    /// Handler Exception when Meilisearch API doesn't send a response message.
    /// </summary>
    /// <param name="statusCode">Status code from http response message.</param>
    /// <param name="reasonPhrase">Reason Phrase from http response message.</param>
    public MeilisearchApiError(HttpStatusCode statusCode, string reasonPhrase)
        : base($"MeilisearchApiError, Message: {reasonPhrase}, Code: {(int)statusCode}")
    {
    }

    /// <summary>
    /// Gets or sets the code return by MeilisearchApi.
    /// </summary>
    public string Code { get; set; }
}
