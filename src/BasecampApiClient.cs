namespace Basecamp3Api;

public partial class BasecampApiClient : IDisposable
{
    public const string AuthUrl = "https://launchpad.37signals.com/authorization/new";
    public const string AuthTokenUrl = "https://launchpad.37signals.com/authorization/token";
    public const string BaseUrl = "https://3.basecampapi.com/";

    private readonly HttpClient _httpClient;
    private readonly BasecampApiSetting _setting;

    public BasecampApiClient(BasecampApiSetting? setting)
    {
        if (setting is null)
            throw new InvalidOperationException("Setting object can not be null");

        if (string.IsNullOrWhiteSpace(setting.ClientId))
            throw new InvalidOperationException("Client id can not be null or empty");

        if (string.IsNullOrWhiteSpace(setting.ClientSecret))
            throw new InvalidOperationException("Client secret can not be null or empty");

        if (string.IsNullOrWhiteSpace(setting.AppName))
            throw new InvalidOperationException("App Name can not be null or empty");

        _setting = setting;
        TokenHasBeenSet = false;
        _httpClient = new HttpClient();
        Accounts = new List<Account>();
    }

    private bool TokenHasBeenSet { get; set; }
    private bool AccountHasBeenSet => Accounts.Any();
    private string? AccessToken { get; set; }
    private string? RefreshToken { get; set; }
    private long ExpiresIn { get; set; }

    private List<Account> Accounts { get; }

    internal Error? ValidateAccount(long accountId)
    {
        if (!_setting.ValidateAccountId) return null;

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

        return null;
    }

    public void Setup(string accessToken, long expiresIn, string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentNullException(nameof(accessToken), "Can not be null or empty");

        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentNullException(nameof(refreshToken), "Can not be null or empty");

        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresIn = expiresIn;

        TokenHasBeenSet = true;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}