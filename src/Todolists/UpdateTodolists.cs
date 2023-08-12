namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Todoset? Todoset, Error? Error)> UpdateTodolistsAsync(
        long accountId,
        long projectId,
        long todolistsId,
        UpdateTodoListsOptions options,
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

        // buckets/1/todolists/3.json
        var pattern = $"{accountId}/buckets/{projectId}/todolists/{todolistsId}.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Put,
            uri,
            new StringContent(
                JsonSerializer.Serialize(options),
                Encoding.UTF8,
                "application/json"
            ));

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<Todoset>(response.Response!.Value.ResultJsonInString);

        return (result, null);
    }
}

public record UpdateTodoListsOptions
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("description")] public string? Description { get; set; }
}