namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> ArchiveTodoAsync(long accountId,
        long projectId,
        long todoId,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            };

        var err = ValidateAccount(accountId);
        if (err != null)
            return err;

        // PUT /buckets/1/recordings/2/status/trashed.json
        var pattern = $"{accountId}/buckets/{projectId}/recordings/{todoId}/status/archived.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Put, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.NoContent, cancellationToken);

        return response.Error;
    }
}