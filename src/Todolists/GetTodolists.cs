namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Todoset? Todoset, Error? Error)> GetTodolistsAsync(
        long accountId,
        long projectId,
        long todolistsId,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            });

        if (!AccountHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Account has not been set"
            });

        if (!Accounts.Any(e => e.Id == accountId))
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Invalid account id"
            });

        // buckets/1/todolists/3.json
        var pattern = $"{accountId}/buckets/{projectId}/todolists/{todolistsId}.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<Todoset>(response.Response!.Value.ResultJsonInString);

        return (result, null);
    }
}