namespace Basecamp3Api.Recordings;

public partial class BasecampApiClient
{
    public Task<Error?> UnarchiveRecordingAsync(
        long accountId,
        long projectId,
        long id,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}