namespace Basecamp3Api.Models;

public record Identity 
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("first_name")] public string? FirstName { get; set; } = null!;

    [JsonPropertyName("last_name")] public string? LastName { get; set; } = null!;

    [JsonPropertyName("email_address")] public string? EmailAddress { get; set; } = null!;
}