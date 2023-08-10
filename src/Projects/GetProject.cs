namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Project> GetProjectAsync(int accountId, long projectId,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        if (!AccountHasBeenSet)
            throw new InvalidOperationException("Account has not been set");

        if (!Accounts.Any(e => e.Id == accountId))
            throw new ArgumentException("Invalid account id", nameof(accountId));

        //$ACCOUNT_ID/projects/1.json
        string pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new UriBuilder(BaseUrl + pattern);

        var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        request.Headers.Add("User-Agent", $"Basecamp 4 Library ({_setting.RedirectUrl})");
        var response = await _httpClient.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Get project", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Project>(content)!;

        return result;
    }
}