namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<PagedList<Project>> GetAllProject(int accountId, int page)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        if (!AccountHasBeenSet)
            throw new InvalidOperationException("Account has not been set");

        if (!Accounts.Any(e => e.Id == accountId))
            throw new ArgumentException("Invalid account id", nameof(accountId));

        //$ACCOUNT_ID/projects.json
        string pattern = $"{accountId}/projects.json";

        var uri = new UriBuilder(BaseUrl + pattern);
        if (page > 1)
        {
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["page"] = page.ToString();
            uri.Query = query.ToString();
        }

        var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        request.Headers.Add("User-Agent", $"Basecamp 4 Library ({_setting.RedirectUrl})");
        var response = await _httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Get all project", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<List<Project>>(content)!;

        response.Headers.TryGetValues("Link", out var values);

        return new PagedList<Project>(result)
        {
            HasNextPage = !string.IsNullOrWhiteSpace(values?.FirstOrDefault())
        };
    }
}