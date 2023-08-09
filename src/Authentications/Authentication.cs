namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// Return login url based on client id and redirect url
    /// </summary>
    /// <param name="uniqueIdentifier"></param>
    /// <returns></returns>
    public string GetLoginUrl(string uniqueIdentifier)
    {
        var uriBuilder = new UriBuilder(AuthUrl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["type"] = "web_server";
        query["client_id"] = _setting.ClientId;
        query["redirect_uri"] = _setting.RedirectUrl!.OriginalString;
        query["state"] = uniqueIdentifier;
        uriBuilder.Query = query.ToString();

        return uriBuilder.ToString();
    }

    /// <summary>
    /// After generate token success, I recommend to use Setup method,
    /// If you play to use another API
    /// </summary>
    /// <param name="code"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Token> GenerateTokenAsync(string code, string identifier)
    {
        var tokenUriBuilder = new UriBuilder(AuthTokenUrl);
        var query = HttpUtility.ParseQueryString(tokenUriBuilder.Query);
        query["type"] = "web_server";
        query["client_id"] = _setting.ClientId;
        query["redirect_uri"] = _setting.RedirectUrl!.OriginalString;
        query["client_secret"] = _setting.ClientSecret;
        query["code"] = code;
        tokenUriBuilder.Query = query.ToString();

        var request = new HttpRequestMessage(HttpMethod.Post, tokenUriBuilder.ToString());

        var response = await _httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Generate Token", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Token>(content)!;

        return result;
    }

    /// <summary>
    /// Get authorization detail user by bearer token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Auth> GetAuthorizationAsync()
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        var uri = new Uri("https://launchpad.37signals.com/authorization.json");

        var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        var response = await _httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Get Authorization", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Auth>(content)!;

        Accounts.Clear();
        Accounts.AddRange(result.Accounts!);

        return result;
    }

    /// <summary>
    /// Generate refresh token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Token> RefreshTokenAsync()
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        //POST https://launchpad.37signals.com/authorization/token?type=refresh&refresh_token=your-current-refresh-token&client_id=your-client-id&redirect_uri=your-redirect-uri&client_secret=your-client-secret

        var tokenUriBuilder = new UriBuilder(AuthTokenUrl);
        var query = HttpUtility.ParseQueryString(tokenUriBuilder.Query);
        query["type"] = "refresh";
        query["client_id"] = _setting.ClientId;
        query["redirect_uri"] = _setting.RedirectUrl!.OriginalString;
        query["client_secret"] = _setting.ClientSecret;
        query["refresh_token"] = RefreshToken;
        tokenUriBuilder.Query = query.ToString();

        var request = new HttpRequestMessage(HttpMethod.Post, tokenUriBuilder.ToString());

        var response = await _httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Refresh Token", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Token>(content)!;

        //re-update current access token
        AccessToken = result.AccessToken;
        ExpiresIn = result.ExpiresIn;

        return result;
    }
}