namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// Get authorization detail user by bearer token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Auth> GetAuthorizationAsync(CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        var uri = new Uri("https://launchpad.37signals.com/authorization.json");

        var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        var response = await _httpClient.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Get Authorization", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Auth>(content)!;

        Accounts.Clear();
        Accounts.AddRange(result.Accounts!);

        return result;
    }
}