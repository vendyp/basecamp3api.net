namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// Get authorization detail user by bearer token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<(Auth? Auth, Error? Error)> GetAuthorizationAsync(CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        var uri = new Uri("https://launchpad.37signals.com/authorization.json");

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);
        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, Error: response.Error);

        var result = JsonSerializer.Deserialize<Auth>(response.Response!.Value.ResultJsonInString)!;

        Accounts.Clear();
        Accounts.AddRange(result.Accounts!);

        return (result, null);
    }
}