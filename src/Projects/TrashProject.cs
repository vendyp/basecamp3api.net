namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> TrashProjectAsync(int accountId, long projectId,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            };

        if (!AccountHasBeenSet)
            return new Error
            {
                StatusCode = -1,
                Message = "Account has not been set"
            };

        if (!Accounts.Any(e => e.Id == accountId))
            return new Error
            {
                StatusCode = -1,
                Message = "Invalid account id"
            };

        //$ACCOUNT_ID/projects/.json
        var pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Delete, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.NoContent, cancellationToken);

        return response.Error;
    }
}