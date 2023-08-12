using System.Collections.Specialized;

namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(PagedList<Todoset>? TodoLists, Error? Error)> GetAllTodolistsAsync(
        long accountId,
        long projectId,
        long todosetId,
        TodolistOptions? options,
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

        options ??= new TodolistOptions();

        var pattern = $"{accountId}/buckets/{projectId}/todosets/{todosetId}/todolists.json";
        var nvc = new NameValueCollection(2)
        {
            ["page"] = options.Page.ToString()
        };
        if (options.Status.HasValue && options.Status.Value != Status.Active)
            nvc["status"] = nameof(options.Status.Value).ToLower();

        var uriBuilder = new UriBuilder(BaseUrl + pattern);
        uriBuilder.AddQueryParams(ConstructQueryString(nvc));

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uriBuilder.Uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var results = JsonSerializer.Deserialize<List<Todoset>>(response.Response!.Value.ResultJsonInString)!;

        return (new PagedList<Todoset>(results)
            {
                HasNextPage = response.Response.Value.Headers.Any(e => e.Key == "Link")
            },
            null);
    }
}

public record TodolistOptions
{
    public int Page { get; set; } = 1;
    public Status? Status { get; set; }
}

public enum Status
{
    Trashed,

    Active,

    Archived
}