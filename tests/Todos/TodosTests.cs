namespace Basecamp3Api.Tests.Todos;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class TodosTests
{
    private readonly BaseFixture _baseFixture;

    public TodosTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task Todos_Cycle_Tests_Should_Do_As_Expected()
    {
        var options = new CreateTodoOptions
        {
            Content = $"Todo {Guid.NewGuid()}"
        };

        var resultCreate = await _baseFixture.Client.CreateTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            6444832541,
            options);

        resultCreate.Error.ShouldBeNull();
        resultCreate.Todos.ShouldNotBeNull();
        resultCreate.Todos.Content.ShouldBe(options.Content);

        var resultGet = await _baseFixture.Client.GetTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todos.ShouldNotBeNull();
        resultGet.Todos.Id.ShouldBe(resultCreate.Todos.Id);


        var updateOptions = new UpdateTodoOptions
        {
            Content = "Test123",
            Description = "Lorep ipsum dolor"
        };

        var resultUpdate = await _baseFixture.Client.UpdateTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id,
            updateOptions);
        resultUpdate.Error.ShouldBeNull();
        resultUpdate.Todos.ShouldNotBeNull();
        resultUpdate.Todos.Id.ShouldBe(resultCreate.Todos.Id);
        resultUpdate.Todos.Content.ShouldBe(updateOptions.Content);
        resultUpdate.Todos.Description.ShouldBe(updateOptions.Description);

        var resultComplete = await _baseFixture.Client.CompleteTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultComplete.ShouldBeNull();

        resultGet = await _baseFixture.Client.GetTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todos.ShouldNotBeNull();
        resultGet.Todos.Id.ShouldBe(resultCreate.Todos.Id);
        resultGet.Todos.Completed.ShouldNotBeNull();
        resultGet.Todos.Completed.Value.ShouldBeTrue();

        var resultUncomplete = await _baseFixture.Client.UncompleteTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultUncomplete.ShouldBeNull();

        resultGet = await _baseFixture.Client.GetTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todos.ShouldNotBeNull();
        resultGet.Todos.Id.ShouldBe(resultCreate.Todos.Id);
        if (resultGet.Todos.Completed.HasValue)
            resultGet.Todos.Completed.Value.ShouldBeFalse();

        var resultTrash = await _baseFixture.Client.TrashTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultTrash.ShouldBeNull();

        resultGet = await _baseFixture.Client.GetTodoAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            resultCreate.Todos.Id);
        resultGet.Error.ShouldBeNull();
        resultGet.Todos.ShouldNotBeNull();
        resultGet.Todos.Id.ShouldBe(resultCreate.Todos.Id);
        resultGet.Todos.Status.ShouldBe(nameof(Status.Trashed).ToLower());
    }
}