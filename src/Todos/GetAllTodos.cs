using System.Collections.Specialized;

namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(PagedList<Todos>? List, Error? Error)> GetAllTodos(long accountId,
        long projectId,
        long todolistsId,
        GetAllTodosOption options,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            });

        var err = ValidateAccount(accountId);
        if (err != null)
            return (null, err);

        var nvc = new NameValueCollection(3)
        {
            ["page"] = options.Page.ToString()
        };
        if (options.Status.HasValue && options.Status.Value != Status.Active)
            nvc["status"] = nameof(options.Status.Value).ToLower();

        if (options.Completed.HasValue)
            nvc["completed"] = options.Completed.Value.ToString().ToLower();

        // GET /buckets/1/todolists/3/todos.json
        var pattern = $"{accountId}/buckets/{projectId}/todolists/{todolistsId}/todos.json";
        var uriBuilder = new UriBuilder(BaseUrl + pattern);
        uriBuilder.AddQueryParams(ConstructQueryString(nvc));

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get,
            uriBuilder.Uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<List<Todos>>(response.Response!.Value.ResultJsonInString)!;

        return (
            new PagedList<Todos>(result) { HasNextPage = response.Response.Value.Headers.Any(e => e.Key == "Link") },
            null);
    }
}

public record GetAllTodosOption
{
    public int Page { get; set; } = 1;
    public Status? Status { get; set; }
    public bool? Completed { get; set; }
}