namespace Basecamp3Api.Models;

public record Creator
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("attachable_sgid")] public string? AttachableSgid { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("email_address")] public string EmailAddress { get; set; } = null!;

    [JsonPropertyName("personable_type")] public string PersonableType { get; set; } = null!;

    [JsonPropertyName("title")] public string Title { get; set; } = null!;

    [JsonPropertyName("bio")] public string Bio { get; set; } = null!;

    [JsonPropertyName("location")] public string Location { get; set; } = null!;

    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("admin")] public bool? Admin { get; set; }

    [JsonPropertyName("owner")] public bool? Owner { get; set; }

    [JsonPropertyName("client")] public bool? Client { get; set; }

    [JsonPropertyName("employee")] public bool? Employee { get; set; }

    [JsonPropertyName("time_zone")] public string TimeZone { get; set; } = null!;

    [JsonPropertyName("avatar_url")] public string AvatarUrl { get; set; } = null!;

    [JsonPropertyName("company")] public Company Company { get; set; } = null!;

    [JsonPropertyName("can_manage_projects")]
    public bool? CanManageProjects { get; set; }

    [JsonPropertyName("can_manage_people")]
    public bool? CanManagePeople { get; set; }
}