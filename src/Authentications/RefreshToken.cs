using System.Collections.Specialized;

namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// Generate refresh token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<(Token? Token, Error? Error)> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        //POST https://launchpad.37signals.com/authorization/token?type=refresh&refresh_token=your-current-refresh-token&client_id=your-client-id&redirect_uri=your-redirect-uri&client_secret=your-client-secret

        var uriBuilder = new UriBuilder(AuthTokenUrl);
        var nvc = new NameValueCollection(5)
        {
            ["type"] = "refresh",
            ["client_id"] = _setting.ClientId,
            ["redirect_uri"] = _setting.RedirectUrl!.OriginalString,
            ["client_secret"] = _setting.ClientSecret,
            ["refresh_token"] = RefreshToken
        };
        uriBuilder.AddQueryParams(ConstructQueryString(nvc));

        var request = CreateRequestMessage(HttpMethod.Post, uriBuilder.Uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, Error: response.Error);

        var result = JsonSerializer.Deserialize<Token>(response.Response!.Value.ResultJsonInString)!;

        //re-update current access token
        AccessToken = result.AccessToken;
        ExpiresIn = result.ExpiresIn;

        return (result, null);
    }
}