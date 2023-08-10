namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task CreateProjectAsync(int accountId, CreateProject data)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        if (!AccountHasBeenSet)
            throw new InvalidOperationException("Account has not been set");

        if (!Accounts.Any(e => e.Id == accountId))
            throw new ArgumentException("Invalid account id", nameof(accountId));

        //$ACCOUNT_ID/projects/.json
        var pattern = $"{accountId}/projects.json";
        var uri = new UriBuilder(BaseUrl + pattern);

        var request = new HttpRequestMessage(HttpMethod.Post, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        request.Headers.Add("User-Agent", $"Basecamp 4 Library ({_setting.RedirectUrl})");
        request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.InsufficientStorage)
            throw new InsufficientStorageException();

        if (response.StatusCode != HttpStatusCode.Created)
            throw new Exception("Result not Created when Create project", new Exception($"With message : {content}"));
    }
}