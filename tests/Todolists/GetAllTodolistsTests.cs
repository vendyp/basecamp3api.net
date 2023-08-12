namespace Basecamp3Api.Tests.Todolists;

[Collection(nameof(BaseFixture))]
public class GetAllTodolistsTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllTodolistsTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAllTodolists_Should_Do_As_Expected()
    {
        var todolists = await _baseFixture.Client.GetAllTodolistsAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            _baseFixture.DockTodosetsId,
            null, CancellationToken.None);

        todolists.Error.ShouldBeNull();
        todolists.TodoLists.ShouldNotBeNull();
    }
}