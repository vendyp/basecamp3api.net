using System.Collections.Specialized;

namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(PagedList<Todos>? Response, Error? Error)> GetRecordingTodoListsAsync(
        long accountId,
        int page,
        long[]? projectIds,
        GetRecodingStatus status = GetRecodingStatus.Active,
        GetRecordingSort sort = GetRecordingSort.Created,
        GetRecordingDirection direction = GetRecordingDirection.Descending,
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

        var response = await GetRecordingsResult(
            accountId,
            page,
            projectIds,
            GetRecordingType.Todolist,
            status,
            sort,
            direction,
            cancellationToken);

        if (response.Error != null)
            return (null, Error: response.Error);

        return (new PagedList<Todos>(
                JsonSerializer.Deserialize<List<Todos>>(response.Response!.Value.ResultJsonInString)!)
            {
                HasNextPage = response.Response.Value.Headers.Any(e => e.Key == "Link")
            },
            null);
    }

    public async Task<(PagedList<Todos>? Response, Error? Error)> GetRecordingTodosAsync(long accountId,
        int page,
        long[]? projectIds,
        GetRecodingStatus status = GetRecodingStatus.Active,
        GetRecordingSort sort = GetRecordingSort.Created,
        GetRecordingDirection direction = GetRecordingDirection.Descending,
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

        var response = await GetRecordingsResult(
            accountId,
            page,
            projectIds,
            GetRecordingType.Todo,
            status,
            sort,
            direction,
            cancellationToken);

        if (response.Error != null)
            return (null, Error: response.Error);

        return (new PagedList<Todos>(
                JsonSerializer.Deserialize<List<Todos>>(response.Response!.Value.ResultJsonInString)!)
            {
                HasNextPage = response.Response.Value.Headers.Any(e => e.Key == "Link")
            },
            null);
    }

    private async Task<(Response? Response, Error? Error)> GetRecordingsResult(long accountId,
        int page,
        long[]? projectIds,
        GetRecordingType type,
        GetRecodingStatus status,
        GetRecordingSort sort,
        GetRecordingDirection direction,
        CancellationToken cancellationToken)
    {
        // GET /projects/recordings.json
        var pattern = $"{accountId}/projects/recordings.json";

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

        return await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);
    }

    public enum GetRecodingStatus
    {
        Active,

        Archived,

        Trashed
    }

    public enum GetRecordingType
    {
        Todo,

        Todolist,
    }

    public enum GetRecordingSort
    {
        Created,

        Updated
    }

    public enum GetRecordingDirection
    {
        Ascending,

        Descending
    }
}