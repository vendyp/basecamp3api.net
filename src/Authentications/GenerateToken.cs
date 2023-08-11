using System.Collections.Specialized;

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
    public async Task<(Token? Token, Error? Error)> GenerateTokenAsync(string code, string identifier,
        CancellationToken cancellationToken = default)
    {
        var uriBuilder = new UriBuilder(AuthTokenUrl);
        var nvc = new NameValueCollection(5)
        {
            ["type"] = "web_server",
            ["client_id"] = _setting.ClientId,
            ["redirect_uri"] = _setting.RedirectUrl!.OriginalString,
            ["client_secret"] = _setting.ClientSecret,
            ["code"] = code
        };
        uriBuilder.AddQueryParams(ConstructQueryString(nvc));

        var request = CreateRequestMessage(HttpMethod.Post, uriBuilder.Uri, null);

        var response = await SendMessageAsync(
            message: request,
            successStatusCode: HttpStatusCode.OK,
            cancellationToken: cancellationToken);

        return response.Error.HasValue
            ? (null, Error: response.Error)
            : (JsonSerializer.Deserialize<Token>(response.Response!.Value.ResultJsonInString), null);
    }
}