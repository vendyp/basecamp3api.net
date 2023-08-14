namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> UncompleteTodoAsync(long accountId,
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

        // DELETE /buckets/1/todos/2/completion.json
        var pattern = $"{accountId}/buckets/{projectId}/todos/{todoId}/completion.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Delete, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.NoContent, cancellationToken);

        return response.Error;
    }
}