namespace Basecamp3Api;

public partial class BasecampApiClient
{
    private readonly string _clientId;
    private readonly string _secretId;

    public BasecampApiClient(string clientId, string secretId)
    {
        if (string.IsNullOrWhiteSpace(clientId))
            throw new InvalidOperationException("Client id can not be null or empty");

        if (string.IsNullOrWhiteSpace(secretId))
            throw new InvalidOperationException("Secret id can not be null or empty");

        _clientId = clientId;
        _secretId = secretId;
    }
}