namespace Basecamp3Api.Tests.Todolists;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class TodolistsTests
{
    private readonly BaseFixture _baseFixture;

    public TodolistsTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task TodoLists_Cycle_Tests_Should_Do_As_Expected()
    {
        //create todolists
        var name = $"Test create todolists vendy {Guid.NewGuid()}";
        const string desc = "Lorep ipsum dolor";
        var resultCreate = await _baseFixture.Client.CreateTodolistsAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            _baseFixture.DockTodosetsId,
            new CreateTodoListsOptions
            {
                Name = name,
                Description = desc
            });

        resultCreate.Error.ShouldBeNull();
        resultCreate.Todoset.ShouldNotBeNull();
        resultCreate.Todoset.Title.ShouldBe(name);
        resultCreate.Todoset.Description.ShouldBe(desc);

        var secondName = $"Test update title/name of todolists vendy {Guid.NewGuid()}";
        var resultUpdate = await _baseFixture.Client.UpdateTodolistsAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id,
            new UpdateTodoListsOptions
            {
                Name = secondName,
                Description = desc
            });
        resultUpdate.Error.ShouldBeNull();
        resultUpdate.Todoset.ShouldNotBeNull();
        resultUpdate.Todoset.Title.ShouldBe(secondName);

        var resultArchived = await _baseFixture.Client.ArchiveRecordingAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id);
        resultArchived.ShouldBeNull();

        var resultGet = await _baseFixture.Client.GetTodolistsAsync(_baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todoset.ShouldNotBeNull();
        resultGet.Todoset.Status.ShouldBe(nameof(Status.Archived).ToLower());

        var resultActive = await _baseFixture.Client.UnarchiveRecordingAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id);
        resultActive.ShouldBeNull();

        resultGet = await _baseFixture.Client.GetTodolistsAsync(_baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todoset.ShouldNotBeNull();
        resultGet.Todoset.Status.ShouldBe(nameof(Status.Active).ToLower());

        var resultTrash = await _baseFixture.Client.TrashRecordingAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id);
        resultTrash.ShouldBeNull();

        resultGet = await _baseFixture.Client.GetTodolistsAsync(_baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todoset.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todoset.ShouldNotBeNull();
        resultGet.Todoset.Status.ShouldBe(nameof(Status.Trashed).ToLower());
    }
}