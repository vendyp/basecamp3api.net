using Basecamp3Api.Models;
using Microsoft.Extensions.Configuration;

namespace Basecamp3Api.Tests;

[CollectionDefinition(nameof(BaseFixture))]
public class BaseFixture : IDisposable, ICollectionFixture<BaseFixture>
{
    /// <summary>
    /// Default selected account id from your account
    /// </summary>
    public int AccountId { get; }

    /// <summary>
    /// Project id or bucket id
    /// </summary>
    public long ProjectId { get; }

    /// <summary>
    /// Dock Message Board Id
    /// </summary>
    public long DockMessageBoardId { get; }

    /// <summary>
    /// Dock todosets id
    /// </summary>
    public long DockTodosetsId { get; }

    /// <summary>
    /// Dock vault id
    /// </summary>
    public long DockVaultId { get; }


    /// <summary>
    /// Campfire
    /// </summary>
    public long DockCampfireId { get; }

    /// <summary>
    /// Schedule
    /// </summary>
    public long DockScheduleId { get; }

    /// <summary>
    /// Quentionare
    /// </summary>
    public long DockQuestionnaireId { get; }

    /// <summary>
    /// Inbox
    /// </summary>
    public long DockInboxId { get; }

    /// <summary>
    /// Kanban
    /// </summary>
    public long DockKanbanId { get; }

    public BaseFixture()
    {
        // get these from get project by id, replace these values with yours then test run will be just fine
        AccountId = 1;
        ProjectId = 1;
        DockMessageBoardId = 1;
        DockTodosetsId = 1;
        DockVaultId = 1;
        DockCampfireId = 1;
        DockScheduleId = 1;
        DockQuestionnaireId = 1;
        DockInboxId = 1;
        DockKanbanId = 1;

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