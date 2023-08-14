namespace Basecamp3Api.Models;

public record Completion
{
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("creator")] public Creator? Creator { get; set; }
}