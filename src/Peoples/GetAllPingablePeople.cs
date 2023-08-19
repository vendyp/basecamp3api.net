namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(List<People>? Peoples, Error? Error)> GetAllPingablePeopleAsync(
        long accountId,
        CancellationToken cancellationToken)
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

        //$ACCOUNT_ID/projects/.json
        var pattern = $"{accountId}/circles/people.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<List<People>>(response.Response!.Value.ResultJsonInString)!;

        return (new List<People>(result), null);
    }
}