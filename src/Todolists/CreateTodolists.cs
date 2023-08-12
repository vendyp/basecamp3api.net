namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Todoset? Todoset, Error? Error)> CreateTodolistsAsync(
        long accountId,
        long projectId,
        long todosetId,
        CreateTodoListsOptions options,
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

        // buckets/1/todosets/3/todolists.json
        var pattern = $"{accountId}/buckets/{projectId}/todosets/{todosetId}/todolists.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Post,
            uri,
            new StringContent(
                JsonSerializer.Serialize(options),
                Encoding.UTF8,
                "application/json"
            ));

        var response = await SendMessageAsync(request, HttpStatusCode.Created, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<Todoset>(response.Response!.Value.ResultJsonInString);

        return (result, null);
    }
}

public record CreateTodoListsOptions
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("description")] public string? Description { get; set; }
}