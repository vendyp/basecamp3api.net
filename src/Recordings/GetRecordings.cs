namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public Task<(PagedList<object>? Response, Error? Error)> GetRecordingsAsync(
        int page,
        GetRecordingType type,
        long[]? projectIds,
        GetRecodingStatus status = GetRecodingStatus.Active,
        GetRecordingSort sort = GetRecordingSort.Created,
        GetRecordingDirection direction = GetRecordingDirection.Descending,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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