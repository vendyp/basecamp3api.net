namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// After generate token success, I recommend to use Setup method,
    /// If you play to use another API
    /// </summary>
    /// <param name="code"></param>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Token> GenerateTokenAsync(string code, string identifier,
        CancellationToken cancellationToken = default)
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

        var response = await _httpClient.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not OK when Generate Token", new Exception($"With message : {content}"));

        var result = JsonSerializer.Deserialize<Token>(content)!;

        return result;
    }
}