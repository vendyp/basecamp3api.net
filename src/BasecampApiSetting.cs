namespace Basecamp3Api;

public class BasecampApiSetting
{
    public BasecampApiSetting()
    {
        ValidateAccountId = true;
    }

    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? AppName { get; set; }
    public Uri? RedirectUrl { get; set; }

    /// <summary>
    /// All parameter account id that passed will be checked through GetAuthorization
    /// </summary>
    public bool ValidateAccountId { get; set; }
}