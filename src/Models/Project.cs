namespace Basecamp3Api.Models;

public record Project
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("status")] public string? Status { get; set; }

    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("description")] public string? Description { get; set; }

    [JsonPropertyName("purpose")] public string? Purpose { get; set; }

    [JsonPropertyName("clients_enabled")] public bool? ClientsEnabled { get; set; }

    [JsonPropertyName("bookmark_url")] public string? BookmarkUrl { get; set; }

    [JsonPropertyName("url")] public string? Url { get; set; }

    [JsonPropertyName("app_url")] public string? AppUrl { get; set; }

    [JsonPropertyName("dock")] public List<Dock> Dock { get; set; } = new();

    [JsonPropertyName("bookmarked")] public bool? Bookmarked { get; set; }
}