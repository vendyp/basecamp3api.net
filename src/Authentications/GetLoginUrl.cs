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
}