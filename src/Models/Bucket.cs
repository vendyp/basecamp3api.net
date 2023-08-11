namespace Basecamp3Api.Models;

public record Bucket
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("type")] public string Type { get; set; } = null!;
}