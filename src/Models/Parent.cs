namespace Basecamp3Api.Models;

public record Parent
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; } = null!;

    [JsonPropertyName("type")] public string Type { get; set; } = null!;

    [JsonPropertyName("url")] public string Url { get; set; } = null!;

    [JsonPropertyName("app_url")] public string AppUrl { get; set; } = null!;
}