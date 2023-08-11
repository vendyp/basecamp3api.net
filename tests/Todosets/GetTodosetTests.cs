namespace Basecamp3Api.Tests.Todosets;

[Collection(nameof(BaseFixture))]
public class GetTodosetTests
{
    private readonly BaseFixture _baseFixture;

    public GetTodosetTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetTodoset_Should_Do_As_Excepted()
    {
        var results = await _baseFixture.Client.GetTodosetAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            _baseFixture.DockTodosetsId,
            CancellationToken.None);
        results.Error.ShouldBeNull();
        results.Todoset.ShouldNotBeNull();
        results.Todoset.Id.ShouldBe(_baseFixture.DockTodosetsId);
    }
}