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

        var typeInString = string.Empty;

        switch (type)
        {
            case GetRecordingType.Todo:
                typeInString = "Todo";
                break;
            case GetRecordingType.Todolist:
                typeInString = "Todolist";
                break;
        }

        var bucketInString = string.Empty;

        if (projectIds != null && projectIds.Length > 0)
            bucketInString = string.Join(',', projectIds);

        string sortInString = string.Empty;
        switch (sort)
        {
            case GetRecordingSort.Created:
                sortInString = sort.ToString().ToLower();
                break;
            case GetRecordingSort.Updated:
                sortInString = sort.ToString().ToLower();
                break;
        }

        string directionInString = string.Empty;

        switch (direction)
        {
            case GetRecordingDirection.Ascending:
                directionInString = "asc";
                break;
            case GetRecordingDirection.Descending:
                directionInString = "desc";
                break;
        }

        var nvc = new NameValueCollection(6)
        {
            ["page"] = page.ToString(),
            ["type"] = typeInString,
            ["bucket"] = bucketInString,
            ["status "] = status.ToString().ToLower(),
            ["sort "] = sortInString,
            ["direction "] = directionInString
        };

        var uri = new UriBuilder(BaseUrl + pattern);
        uri.AddQueryParams(ConstructQueryString(nvc));

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