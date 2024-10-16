using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Meilisearch;

/// <summary>
/// The json converter factory for <see cref="ISearchable{T}"/>
/// </summary>
public class ISearchableJsonConverterFactory : JsonConverterFactory
{
    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert is { IsInterface: true, IsGenericType: true }
               && (typeToConvert.GetGenericTypeDefinition() == typeof(ISearchable<>));
    }

    /// <inheritdoc/>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var genericArgs = typeToConvert.GetGenericArguments();
        var converterType = typeof(ISearchableJsonConverter<>).MakeGenericType(
            genericArgs[0]
        );
        var converter = (JsonConverter)Activator.CreateInstance(converterType);
        return converter;
    }
}

/// <summary>
/// The json converter for <see cref="ISearchable{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public class ISearchableJsonConverter<T> : JsonConverter<ISearchable<T>> where T : class
{
    /// <inheritdoc/>
    public override ISearchable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var document = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        return document.TryGetProperty("page", out _) || document.TryGetProperty("hitsPerPage", out _)
            ? document.Deserialize<PaginatedSearchResult<T>>(options)
            : document.Deserialize<SearchResult<T>>(options);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ISearchable<T> value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case PaginatedSearchResult<T> paginated:
                JsonSerializer.Serialize(writer, paginated, options);
                break;
            case SearchResult<T> normal:
                JsonSerializer.Serialize(writer, normal, options);
                break;
            default:
                JsonSerializer.Serialize<object>(writer, value, options);
                break;
        }
    }
}
