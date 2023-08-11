using Basecamp3Api.Models;
using Microsoft.Extensions.Configuration;

namespace Basecamp3Api.Tests;

[CollectionDefinition(nameof(BaseFixture))]
public class BaseFixture : IDisposable, ICollectionFixture<BaseFixture>
{
    public BaseFixture()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("basecamp.json")
            .AddJsonFile("appsettings.json")
            .Build();

        var s1 = configuration.GetValue<string>("access_token");
        var s2 = configuration.GetValue<long>("expires_in");
        var s3 = configuration.GetValue<string>("refresh_token");

        Setting = new Token
        {
            AccessToken = s1!,
            RefreshToken = s3!,
            ExpiresIn = s2
        };

        var dto = new BasecampApiSetting
        {
            ClientId = configuration.GetValue<string>("ClientId"),
            ClientSecret = configuration.GetValue<string>("ClientSecretId"),
            RedirectUrl = new Uri(configuration.GetValue<string>("RedirectUrl")!),
            AppName = configuration.GetValue<string>("AppName"),
        };

        Client = new BasecampApiClient(dto);
        Client.Setup(Setting.AccessToken, Setting.ExpiresIn, Setting.RefreshToken);
    }

    public BasecampApiClient Client { get; }

    public Token Setting { get; set; }

    public void Dispose()
    {
        Client.Dispose();
    }
}