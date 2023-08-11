namespace Basecamp3Api.Models;

public record Todoset
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; } = null!;

    [JsonPropertyName("visible_to_clients")]
    public bool? VisibleToClients { get; set; }

    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; } = null!;

    [JsonPropertyName("inherits_status")] public bool? InheritsStatus { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; } = null!;

    [JsonPropertyName("url")] public string Url { get; set; } = null!;

    [JsonPropertyName("app_url")] public string AppUrl { get; set; } = null!;

    [JsonPropertyName("bookmark_url")] public string BookmarkUrl { get; set; } = null!;

    [JsonPropertyName("position")] public int? Position { get; set; }

    [JsonPropertyName("bucket")] public Bucket Bucket { get; set; } = null!;

    [JsonPropertyName("creator")] public Creator Creator { get; set; } = null!;

    [JsonPropertyName("completed")] public bool? Completed { get; set; }

    [JsonPropertyName("completed_ratio")] public string CompletedRatio { get; set; } = null!;

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("todolists_count")] public int? TodolistsCount { get; set; }

    [JsonPropertyName("todolists_url")] public string TodolistsUrl { get; set; } = null!;

    [JsonPropertyName("app_todoslists_url")]
    public string AppTodosListUrl { get; set; } = null!;
}