namespace Basecamp3Api.Tests.Recordings;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetAllRecordingTodosTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllRecordingTodosTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAllRecordingTodos_Should_Do_As_Expected()
    {
        var todos =
            await _baseFixture.Client.GetRecordingTodosAsync(
                _baseFixture.AccountId,
                1,
                null);
        todos.Error.ShouldBeNull();
        todos.Response.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetAllRecordingTodoLists_Should_Do_As_Expected()
    {
        var todos =
            await _baseFixture.Client.GetRecordingTodoListsAsync(
                _baseFixture.AccountId,
                1,
                null);
        todos.Error.ShouldBeNull();
        todos.Response.ShouldNotBeNull();
    }
}