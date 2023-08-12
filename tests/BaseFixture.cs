using Basecamp3Api.Models;
using Microsoft.Extensions.Configuration;

namespace Basecamp3Api.Tests;

[CollectionDefinition(nameof(BaseFixture))]
public class BaseFixture : IDisposable, ICollectionFixture<BaseFixture>
{
    /// <summary>
    /// Default selected account id from your account
    /// </summary>
    public long AccountId { get; }

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

        //setup testing selected account id, project id, and others dock(s) id
        //from appsettings.json
        var accountId = configuration.GetValue<string>("AccountId");
        if (!string.IsNullOrWhiteSpace(accountId) && long.TryParse(accountId, out var accId))
            AccountId = accId;

        var projectId = configuration.GetValue<string>("ProjectId");
        if (!string.IsNullOrWhiteSpace(projectId) && long.TryParse(projectId, out var projId))
            ProjectId = projId;

        var messageBoardId = configuration.GetValue<string>("MessageBoardId");
        if (!string.IsNullOrWhiteSpace(messageBoardId) && long.TryParse(messageBoardId, out var messBoardId))
            DockMessageBoardId = messBoardId;

        var todoSetsId = configuration.GetValue<string>("TodosetsId");
        if (!string.IsNullOrWhiteSpace(todoSetsId) && long.TryParse(todoSetsId, out var tdSetsId))
            DockTodosetsId = tdSetsId;

        var vaultId = configuration.GetValue<string>("VaultId");
        if (!string.IsNullOrWhiteSpace(vaultId) && long.TryParse(vaultId, out var valId))
            DockVaultId = valId;

        var campfireId = configuration.GetValue<string>("CampfireId");
        if (!string.IsNullOrWhiteSpace(campfireId) && long.TryParse(campfireId, out var campId))
            DockCampfireId = campId;

        var scheduleId = configuration.GetValue<string>("ScheduleId");
        if (!string.IsNullOrWhiteSpace(scheduleId) && long.TryParse(scheduleId, out var schId))
            DockScheduleId = schId;

        var questionnaireId = configuration.GetValue<string>("QuestionnaireId");
        if (!string.IsNullOrWhiteSpace(questionnaireId) && long.TryParse(questionnaireId, out var qId))
            DockQuestionnaireId = qId;

        var inboxId = configuration.GetValue<string>("InboxId");
        if (!string.IsNullOrWhiteSpace(inboxId) && long.TryParse(inboxId, out var iId))
            DockInboxId = iId;

        var kanbanId = configuration.GetValue<string>("KanbanId");
        if (!string.IsNullOrWhiteSpace(kanbanId) && long.TryParse(kanbanId, out var kId))
            DockKanbanId = kId;

        Client = new BasecampApiClient(dto);
        Client.Setup(Setting.AccessToken, Setting.ExpiresIn, Setting.RefreshToken);
        _ = Client.GetAuthorizationAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public BasecampApiClient Client { get; }

    public Token Setting { get; set; }

    public void Dispose()
    {
        Client.Dispose();
    }
}