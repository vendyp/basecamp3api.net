using System.Collections.Specialized;

namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(PagedList<Project>? List, Error? Error)> GetAllProjectAsync(
        long accountId, 
        int page,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            });

        if (!AccountHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Account has not been set"
            });

        if (!Accounts.Any(e => e.Id == accountId))
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Invalid account id"
            });

        //$ACCOUNT_ID/projects.json
        var pattern = $"{accountId}/projects.json";

        var uri = new UriBuilder(BaseUrl + pattern);
        if (page > 1)
        {
            var nvc = new NameValueCollection(1)
            {
                ["page"] = page.ToString()
            };
            uri.AddQueryParams(ConstructQueryString(nvc));
        }

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri.Uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, Error: response.Error);

        var result = JsonSerializer.Deserialize<List<Project>>(response.Response!.Value.ResultJsonInString)!;

        return (new PagedList<Project>(result)
            {
                HasNextPage = response.Response.Value.Headers.Any(e => e.Key == "Link")
            },
            null);
    }
}