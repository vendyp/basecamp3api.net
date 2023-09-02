namespace Basecamp3Api.Models;

public record Todos 
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; } = null!;

    [JsonPropertyName("visible_to_clients")]
    public bool? VisibleToClients { get; set; }

    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; } = null!;

    [JsonPropertyName("inherits_status")] public bool? InheritsStatus { get; set; }

    [JsonPropertyName("type")] public string? Type { get; set; } = null!;

    [JsonPropertyName("url")] public string? Url { get; set; } = null!;

    [JsonPropertyName("app_url")] public string? AppUrl { get; set; } = null!;

    [JsonPropertyName("bookmark_url")] public string? BookmarkUrl { get; set; } = null!;

    [JsonPropertyName("position")] public int? Position { get; set; }

    [JsonPropertyName("bucket")] public Bucket Bucket { get; set; } = null!;

    [JsonPropertyName("creator")] public Creator Creator { get; set; } = null!;

    [JsonPropertyName("parent")] public Parent? Parent { get; set; }

    [JsonPropertyName("company")] public Company? Company { get; set; }

    [JsonPropertyName("completed")] public bool? Completed { get; set; }

    [JsonPropertyName("completed_ratio")] public string? CompletedRatio { get; set; } = null!;

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("todolists_count")] public int? TodolistsCount { get; set; }

    [JsonPropertyName("todolists_url")] public string? TodolistsUrl { get; set; } = null!;

    [JsonPropertyName("app_todoslists_url")]
    public string AppTodosListUrl { get; set; } = null!;

    [JsonPropertyName("description")] public string? Description { get; set; }

    [JsonPropertyName("subscription_url")] public string? SubscriptionUrl { get; set; }

    [JsonPropertyName("comments_count")] public long? CommentsCount { get; set; }

    [JsonPropertyName("comments_url")] public string? CommentsUrl { get; set; }

    [JsonPropertyName("bio")] public string? Bio { get; set; }

    [JsonPropertyName("group_position_url")]
    public string? GroupPositionUrl { get; set; }

    [JsonPropertyName("content")] public string? Content { get; set; }

    [JsonPropertyName("start_on")] public DateTime? StartOn { get; set; }

    [JsonPropertyName("due_on")] public DateTime? DueOn { get; set; }

    [JsonPropertyName("completion")] public Completion? Completion { get; set; }

    [JsonPropertyName("assignees")] public List<People>? Assignees { get; set; }

    [JsonPropertyName("completion_subscribers")]
    public List<People>? Subscribers { get; set; }
}