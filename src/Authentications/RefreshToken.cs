namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// Generate refresh token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Token> RefreshTokenAsync(CancellationToken cancellationToken = default)
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

        var response = await _httpClient.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Refresh Token", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Token>(content)!;

        //re-update current access token
        AccessToken = result.AccessToken;
        ExpiresIn = result.ExpiresIn;

        return result;
    }
}