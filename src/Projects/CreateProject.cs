namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> CreateProjectAsync(
        long accountId,
        CreateProject data,
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

        //$ACCOUNT_ID/projects/.json
        var pattern = $"{accountId}/projects.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Post, uri,
            new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));

        var response = await SendMessageAsync(request, HttpStatusCode.Created, cancellationToken);

        return response.Error;
    }
}