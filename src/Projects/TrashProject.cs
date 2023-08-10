namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task TrashProjectAsync(int accountId, long projectId, CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        if (!AccountHasBeenSet)
            throw new InvalidOperationException("Account has not been set");

        if (!Accounts.Any(e => e.Id == accountId))
            throw new ArgumentException("Invalid account id", nameof(accountId));

        //$ACCOUNT_ID/projects/.json
        var pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new UriBuilder(BaseUrl + pattern);

        var request = new HttpRequestMessage(HttpMethod.Delete, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        request.Headers.Add("User-Agent", $"Basecamp 4 Library ({_setting.RedirectUrl})");
        var response = await _httpClient.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.NoContent)
            throw new Exception("Result not Created when Trash project", new Exception($"With message : {content}"));
    }
}