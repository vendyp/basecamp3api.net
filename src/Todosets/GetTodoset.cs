namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Todos? Todoset, Error? Error)> GetTodosetAsync(long accountId, long projectId, long todosetId,
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

        // buckets/1/todosets/2.json
        var pattern = $"{accountId}/buckets/{projectId}/todosets/{todosetId}.json";

        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<Todos>(response.Response!.Value.ResultJsonInString)!;

        return (result, null);
    }
}