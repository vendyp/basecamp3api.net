namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Todos? Todos, Error? Error)> UpdateTodoAsync(
        long accountId,
        long projectId,
        long todolistsId,
        UpdateTodoOptions options,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(options.Content))
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Content can not be null or empty"
            });

        if (!TokenHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            });

        var err = ValidateAccount(accountId);
        if (err != null)
            return (null, err);

        // PUT /buckets/1/todos/2.json
        var pattern = $"{accountId}/buckets/{projectId}/todos/{todolistsId}.json";
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

        var result = JsonSerializer.Deserialize<Todos>(response.Response!.Value.ResultJsonInString);

        return (result, null);
    }
}

public record UpdateTodoOptions
{
    [JsonPropertyName("content")] public string Content { get; set; } = null!;
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("notify")] public bool Notify { get; set; }
    [JsonPropertyName("assignee_ids")] public List<long>? AssigneeIds { get; set; }

    [JsonPropertyName("completion_subscriber_ids")]
    public List<long>? SubscriberIds { get; set; }

    [JsonPropertyName("start_on")] public DateTime? StartOn { get; set; }

    [JsonPropertyName("due_on")] public DateTime? DueOn { get; set; }
}