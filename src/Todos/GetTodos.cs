namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Todos? Todos, Error? Error)> GetTodoAsync(long accountId,
        long projectId,
        long todoId,
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

        // GET /buckets/1/todos/2.json
        var pattern = $"{accountId}/buckets/{projectId}/todos/{todoId}.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        var result = JsonSerializer.Deserialize<Todos>(response.Response!.Value.ResultJsonInString);

        return (result, null);
    }
}