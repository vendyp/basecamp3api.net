namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> RepositionTodoAsync(long accountId,
        long projectId,
        long todoId,
        int position,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            };

        if (position < 1)
            return new Error
            {
                StatusCode = -1,
                Message = "Position must greater than 0"
            };

        var err = ValidateAccount(accountId);
        if (err != null)
            return err;

        // PUT /buckets/1/todos/2/position.json
        var pattern = $"{accountId}/buckets/{projectId}/todos/{todoId}/position.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Put, uri, new StringContent(
            JsonSerializer.Serialize(new
            {
                position
            }), Encoding.UTF8, "application/json"));

        var response = await SendMessageAsync(request, HttpStatusCode.NoContent, cancellationToken);

        return response.Error;
    }
}