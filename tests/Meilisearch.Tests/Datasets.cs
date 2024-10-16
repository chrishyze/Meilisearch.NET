using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Meilisearch.Tests;

internal static class Datasets
{
    private static readonly string BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Datasets");
    public static readonly string SmallMoviesJsonPath = Path.Combine(BasePath, "small_movies.json");
    public static readonly string SongsCsvPath = Path.Combine(BasePath, "songs.csv");
    public static readonly string SongsCsvCustomDelimiterPath = Path.Combine(BasePath, "songs_custom_delimiter.csv");
    public static readonly string SongsNdjsonPath = Path.Combine(BasePath, "songs.ndjson");

    public static readonly string MoviesWithStringIdJsonPath = Path.Combine(BasePath, "movies_with_string_id.json");
    public static readonly string MoviesForFacetingJsonPath = Path.Combine(BasePath, "movies_for_faceting.json");
    public static readonly string MoviesWithIntIdJsonPath = Path.Combine(BasePath, "movies_with_int_id.json");
    public static readonly string MoviesWithInfoJsonPath = Path.Combine(BasePath, "movies_with_info.json");

    public static readonly string ProductsForDistinctJsonPath =
        Path.Combine(BasePath, "products_for_distinct_search.json");
}

public class DatasetSmallMovie
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Poster { get; set; }
    public string Overview { get; set; }

    [JsonPropertyName("release_date")]
    [JsonConverter(typeof(UnixEpochDateTimeConverter))]
    public DateTime ReleaseDate { get; set; }

    public string Genre { get; set; }
}

public class DatasetSong
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Album { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }
    public string Country { get; set; }
    public string Released { get; set; }
    public string Duration { get; set; }

    [JsonPropertyName("released-timestamp")]
    [JsonConverter(typeof(NullableInt64Converter))]
    public long? ReleasedTimestamp { get; set; }

    [JsonPropertyName("duration-float")]
    [JsonConverter(typeof(NullableDoubleConverter))]
    public double? DurationFloat { get; set; }
}

sealed class UnixEpochDateTimeConverter : JsonConverter<DateTime>
{
    static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0);

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var unixTime = reader.GetInt64();
        return Epoch.AddMilliseconds(unixTime);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var unixTime = Convert.ToInt64((value - Epoch).TotalMilliseconds);
        writer.WriteNumberValue(unixTime);
    }
}

sealed class NullableInt64Converter : JsonConverter<Int64?>
{
    public override Int64? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        long value;
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return reader.TryGetInt64(out value) ? value : null;
            case JsonTokenType.String:
                return Int64.TryParse(reader.GetString(), out value) ? value : null;
            case JsonTokenType.None:
            case JsonTokenType.StartObject:
            case JsonTokenType.EndObject:
            case JsonTokenType.StartArray:
            case JsonTokenType.EndArray:
            case JsonTokenType.PropertyName:
            case JsonTokenType.Comment:
            case JsonTokenType.True:
            case JsonTokenType.False:
            case JsonTokenType.Null:
            default:
                return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, Int64? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Convert.ToString(value));
    }
}

sealed class NullableDoubleConverter : JsonConverter<Double?>
{
    public override Double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        double value;
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return reader.TryGetDouble(out value) ? value : null;
            case JsonTokenType.String:
                return Double.TryParse(reader.GetString(), out value) ? value : null;
            case JsonTokenType.None:
            case JsonTokenType.StartObject:
            case JsonTokenType.EndObject:
            case JsonTokenType.StartArray:
            case JsonTokenType.EndArray:
            case JsonTokenType.PropertyName:
            case JsonTokenType.Comment:
            case JsonTokenType.True:
            case JsonTokenType.False:
            case JsonTokenType.Null:
            default:
                return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, Double? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Convert.ToString(value));
    }
}
