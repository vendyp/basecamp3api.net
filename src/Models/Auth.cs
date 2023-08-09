namespace Basecamp3Api.Models;

public class Auth
{
    [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("identity")] public Identity? Identity { get; set; }

    [JsonPropertyName("accounts")] public List<Account>? Accounts { get; set; }
}