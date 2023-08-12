namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(Project? Project, Error? Error)> GetProjectAsync(
        long accountId, 
        long projectId,
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

        //$ACCOUNT_ID/projects/1.json   
        var pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new Uri(BaseUrl + pattern);
        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<Project>(response.Response!.Value.ResultJsonInString)!;

        return (result, null);
    }
}