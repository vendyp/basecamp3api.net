namespace Basecamp3Api.Models;

public class Account
{
    [JsonPropertyName("product")] public string Product { get; set; } = null!;

    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("href")] public string Href { get; set; } = null!;

    [JsonPropertyName("app_href")] public string AppHref { get; set; } = null!;
}