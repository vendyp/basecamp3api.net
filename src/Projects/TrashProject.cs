namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> TrashProjectAsync(
        long accountId, 
        long projectId,
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
        var pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Delete, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.NoContent, cancellationToken);

        return response.Error;
    }
}