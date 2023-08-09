namespace Basecamp3Api.Models;

public record Dock
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("title")] public string? Title { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }

    [JsonPropertyName("position")] public int? Position { get; set; }

    [JsonPropertyName("url")] public string? Url { get; set; }

    [JsonPropertyName("app_url")] public string? AppUrl { get; set; }
}