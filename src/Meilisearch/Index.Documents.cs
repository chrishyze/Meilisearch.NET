using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Meilisearch.Errors;
using Meilisearch.Extensions;
using Meilisearch.QueryParameters;

namespace Meilisearch
{
    public partial class Index
    {
        /// <summary>
        /// Add documents.
        /// </summary>
        /// <param name="documents">Documents to add.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type of the document. Even though documents are schemaless in Meilisearch, making it typed helps in compile time.</typeparam>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> AddDocumentsAsync<T>(IEnumerable<T> documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            HttpResponseMessage responseMessage;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            responseMessage = await _http
                .PostJsonCustomAsync(uri, documents, options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add documents from JSON string.
        /// </summary>
        /// <param name="documents">Documents to add as JSON string.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> AddDocumentsJsonAsync(string documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            var content = new StringContent(documents, Encoding.UTF8, ContentType.Json);
            var responseMessage = await _http.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add documents from CSV string.
        /// </summary>
        /// <param name="documents">Documents to add as CSV string.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="csvDelimiter">One ASCII character used to customize the delimiter for CSV. Comma used by default.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> AddDocumentsCsvAsync(string documents, string primaryKey = default,
            char csvDelimiter = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents";
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if (primaryKey != default)
            {
                queryString.Add("primaryKey", primaryKey);
            }

            if (csvDelimiter != default)
            {
                queryString.Add("csvDelimiter", csvDelimiter.ToString());
            }

            uri = $"{uri}?{queryString}";

            var content = new StringContent(documents, Encoding.UTF8, ContentType.Csv);
            var responseMessage = await _http.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add documents from NDJSON string.
        /// </summary>
        /// <param name="documents">Documents to add as NDJSON string.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> AddDocumentsNdjsonAsync(string documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            var content = new StringContent(documents, Encoding.UTF8, ContentType.Ndjson);
            var responseMessage = await _http.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Adds documents in batches with size specified with <paramref name="batchSize"/>.
        /// </summary>
        /// <param name="documents">Documents to add.</param>
        /// <param name="batchSize">Size of documents batches while adding them.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type of the document. Even though documents are schemaless in Meilisearch, making it typed helps in compile time.</typeparam>
        /// <returns>Returns the task list.</returns>
        public async Task<IEnumerable<TaskInfo>> AddDocumentsInBatchesAsync<T>(IEnumerable<T> documents,
            int batchSize = 1000, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var tasks = new List<TaskInfo>();
            foreach (var chunk in documents.GetChunks(batchSize))
            {
                tasks.Add(await AddDocumentsAsync(chunk, primaryKey, options, cancellationToken).ConfigureAwait(false));
            }

            return tasks;
        }

        /// <summary>
        /// Adds documents from CSV string in batches with size specified with <paramref name="batchSize"/>.
        /// </summary>
        /// <param name="documents">Documents to add as CSV string.</param>
        /// <param name="batchSize">Size of documents batches while adding them.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="csvDelimiter">One ASCII character used to customize the delimiter for CSV. Comma used by default.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task list.</returns>
        public async Task<IEnumerable<TaskInfo>> AddDocumentsCsvInBatchesAsync(string documents,
            int batchSize = 1000, string primaryKey = default, char csvDelimiter = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var tasks = new List<TaskInfo>();
            foreach (var chunk in documents.GetCsvChunks(batchSize))
            {
                tasks.Add(await AddDocumentsCsvAsync(chunk, primaryKey, csvDelimiter, options, cancellationToken)
                    .ConfigureAwait(false));
            }

            return tasks;
        }

        /// <summary>
        /// Adds documents from NDJSON string in batches with size specified with <paramref name="batchSize"/>.
        /// </summary>
        /// <param name="documents">Documents to add as NDJSON string.</param>
        /// <param name="batchSize">Size of documents batches while adding them.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task list.</returns>
        public async Task<IEnumerable<TaskInfo>> AddDocumentsNdjsonInBatchesAsync(string documents,
            int batchSize = 1000, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var tasks = new List<TaskInfo>();
            foreach (var chunk in documents.GetNdjsonChunks(batchSize))
            {
                tasks.Add(await AddDocumentsNdjsonAsync(chunk, primaryKey, options, cancellationToken)
                    .ConfigureAwait(false));
            }

            return tasks;
        }

        /// <summary>
        /// Update documents.
        /// </summary>
        /// <param name="documents">Documents to update.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type of document. Even though documents are schemaless in Meilisearch, making it typed helps in compile time.</typeparam>
        /// <returns>Returns the task list.</returns>
        public async Task<TaskInfo> UpdateDocumentsAsync<T>(IEnumerable<T> documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            HttpResponseMessage responseMessage;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            responseMessage = await _http
                .PutJsonCustomAsync(uri, documents, options, cancellationToken)
                .ConfigureAwait(false);

            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Update documents from JSON string.
        /// </summary>
        /// <param name="documents">Documents to add as JSON string.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> UpdateDocumentsJsonAsync(string documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            var content = new StringContent(documents, Encoding.UTF8, ContentType.Json);
            var responseMessage = await _http.PutAsync(uri, content, cancellationToken).ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Update documents from CSV string.
        /// </summary>
        /// <param name="documents">Documents to add as CSV string.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> UpdateDocumentsCsvAsync(string documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            var content = new StringContent(documents, Encoding.UTF8, ContentType.Csv);
            var responseMessage = await _http.PutAsync(uri, content, cancellationToken).ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Update documents from NDJSON string.
        /// </summary>
        /// <param name="documents">Documents to add as NDJSON string.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> UpdateDocumentsNdjsonAsync(string documents, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents";

            if (primaryKey != default)
            {
                uri = $"{uri}?{new { primaryKey = primaryKey }.ToQueryString()}";
            }

            var content = new StringContent(documents, Encoding.UTF8, ContentType.Ndjson);
            var responseMessage = await _http.PutAsync(uri, content, cancellationToken).ConfigureAwait(false);
            return await responseMessage.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates documents in batches with size specified with <paramref name="batchSize"/>.
        /// </summary>
        /// <param name="documents">Documents to update.</param>
        /// <param name="batchSize">Size of documents batches while updating them.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type of the document. Even though documents are schemaless in Meilisearch, making it typed helps in compile time.</typeparam>
        /// <returns>Returns the task list.</returns>
        public async Task<IEnumerable<TaskInfo>> UpdateDocumentsInBatchesAsync<T>(IEnumerable<T> documents,
            int batchSize = 1000, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var tasks = new List<TaskInfo>();
            foreach (var chunk in documents.GetChunks(batchSize))
            {
                tasks.Add(await UpdateDocumentsAsync(chunk, primaryKey, options, cancellationToken)
                    .ConfigureAwait(false));
            }

            return tasks;
        }

        /// <summary>
        /// Updates documents as CSV string in batches with size specified with <paramref name="batchSize"/>.
        /// </summary>
        /// <param name="documents">Documents to update from CSV string.</param>
        /// <param name="batchSize">Size of documents batches while updating them.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task list.</returns>
        public async Task<IEnumerable<TaskInfo>> UpdateDocumentsCsvInBatchesAsync(string documents,
            int batchSize = 1000, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var tasks = new List<TaskInfo>();
            foreach (var chunk in documents.GetCsvChunks(batchSize))
            {
                tasks.Add(await UpdateDocumentsCsvAsync(chunk, primaryKey, options, cancellationToken)
                    .ConfigureAwait(false));
            }

            return tasks;
        }

        /// <summary>
        /// Updates documents as NDJSON string in batches with size specified with <paramref name="batchSize"/>.
        /// </summary>
        /// <param name="documents">Documents to update from NDJSON string.</param>
        /// <param name="batchSize">Size of documents batches while updating them.</param>
        /// <param name="primaryKey">Primary key for the documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task list.</returns>
        public async Task<IEnumerable<TaskInfo>> UpdateDocumentsNdjsonInBatchesAsync(string documents,
            int batchSize = 1000, string primaryKey = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var tasks = new List<TaskInfo>();
            foreach (var chunk in documents.GetNdjsonChunks(batchSize))
            {
                tasks.Add(await UpdateDocumentsNdjsonAsync(chunk, primaryKey, options, cancellationToken)
                    .ConfigureAwait(false));
            }

            return tasks;
        }

        /// <summary>
        /// Get document by its ID.
        /// </summary>
        /// <param name="documentId">Document identifier.</param>
        /// <param name="fields">Document attributes to show (case-sensitive).</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type of the document.</typeparam>
        /// <returns>Returns the document, with the according type if the object is available.</returns>
        public async Task<T> GetDocumentAsync<T>(string documentId, List<string> fields = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var uri = $"indexes/{Uid}/documents/{documentId}";
            if (fields != null)
            {
                uri = $"{uri}?fields={string.Join(",", fields)}";
            }

            return await _http
                .GetFromJsonAsync<T>(uri, options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get document by its ID.
        /// </summary>
        /// <param name="documentId">Document Id for query.</param>
        /// <param name="fields">Document attributes to show (case-sensitive).</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type to return for document.</typeparam>
        /// <returns>Type if the object is availble.</returns>
        public async Task<T> GetDocumentAsync<T>(int documentId, List<string> fields = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await GetDocumentAsync<T>(documentId.ToString(), fields, options, cancellationToken);
        }

        /// <summary>
        /// Get documents with the allowed Query Parameters.
        /// </summary>
        /// <param name="query">Query parameters supports by the method.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type of the document.</typeparam>
        /// <returns>Returns the list of documents.</returns>
        public async Task<ResourceResults<IEnumerable<T>>> GetDocumentsAsync<T>(DocumentsQuery query = default,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            if (query is { Filter: not null })
            {
                try
                {
                    //Use the fetch route
                    var uri = $"indexes/{Uid}/documents/fetch";
                    var result = await _http.PostAsJsonAsync(uri, query, options,
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                    return await result.Content
                        .ReadFromJsonAsync<ResourceResults<IEnumerable<T>>>(options: options,
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (MeilisearchCommunicationError e)
                {
                    throw new MeilisearchCommunicationError(
                        Constants.VersionErrorHintMessage(e.Message, nameof(GetDocumentsAsync)), e);
                }
            }
            else
            {
                var uri = query.ToQueryString(uri: $"indexes/{Uid}/documents");

                return await _http
                    .GetFromJsonAsync<ResourceResults<IEnumerable<T>>>(uri, options: options,
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete one document.
        /// </summary>
        /// <param name="documentId">Document identifier.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> DeleteOneDocumentAsync(string documentId,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/documents/{documentId}", cancellationToken)
                .ConfigureAwait(false);
            return await httpresponse.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Delete one document by its ID.
        /// </summary>
        /// <param name="documentId">document ID.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> DeleteOneDocumentAsync(int documentId,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            return await DeleteOneDocumentAsync(documentId.ToString(), options, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Delete documents in batch.
        /// </summary>
        /// <param name="documentIds">List of documents identifier.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> DeleteDocumentsAsync(IEnumerable<string> documentIds,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var httpresponse =
                await _http.PostAsJsonAsync($"indexes/{Uid}/documents/delete-batch", documentIds,
                        options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            return await httpresponse.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Delete documents from an index based on a filter.
        /// </summary>
        /// <remarks>Available ONLY with Meilisearch v1.2 and newer.</remarks>
        /// <param name="query">A hash containing a filter that should match documents.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Return the task info.</returns>
        public async Task<TaskInfo> DeleteDocumentsAsync(DeleteDocumentsQuery query,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            try
            {
                var httpresponse =
                    await _http.PostAsJsonAsync($"indexes/{Uid}/documents/delete", query,
                            options: options, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                return await httpresponse.Content
                    .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (MeilisearchCommunicationError e)
            {
                throw new MeilisearchCommunicationError(
                    Constants.VersionErrorHintMessage(e.Message, nameof(DeleteDocumentsAsync)), e);
            }
        }

        /// <summary>
        /// Delete documents in batch.
        /// </summary>
        /// <param name="documentIds">List of document Id.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Return the task info.</returns>
        public async Task<TaskInfo> DeleteDocumentsAsync(IEnumerable<int> documentIds,
            JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var docIds = documentIds.Select(id => id.ToString());
            return await DeleteDocumentsAsync(docIds, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete all the documents in the index.
        /// </summary>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <returns>Returns the task info.</returns>
        public async Task<TaskInfo> DeleteAllDocumentsAsync(JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            var httpresponse = await _http.DeleteAsync($"indexes/{Uid}/documents", cancellationToken)
                .ConfigureAwait(false);
            return await httpresponse.Content
                .ReadFromJsonAsync<TaskInfo>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Search documents according to search parameters.
        /// </summary>
        /// <param name="query">Query Parameter with Search.</param>
        /// <param name="searchAttributes">Attributes to search.</param>
        /// <param name="options">The JSON serialization options.</param>
        /// <param name="cancellationToken">The cancellation token for this call.</param>
        /// <typeparam name="T">Type parameter to return.</typeparam>
        /// <returns>Returns Enumerable of items.</returns>
        public async Task<ISearchable<T>> SearchAsync<T>(string query,
            SearchQuery searchAttributes = default(SearchQuery), JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= Constants.JsonSerializerOptionsRemoveNulls;
            SearchQuery body;
            if (searchAttributes == null)
            {
                body = new SearchQuery { Q = query };
            }
            else
            {
                body = searchAttributes;
                body.Q = query;
            }

            body.IndexUid = default;

            var responseMessage = await _http.PostAsJsonAsync($"indexes/{Uid}/search", body,
                    options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return await responseMessage.Content
                .ReadFromJsonAsync<ISearchable<T>>(options: options, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
