namespace Basecamp3Api.Tests.Todos;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetAllTodosTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllTodosTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAllTodos_Should_Do_As_Expected()
    {
        var resultList = await _baseFixture.Client.GetAllTodos(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            6444832541,
            new GetAllTodosOption { Page = 1 });

        resultList.Error.ShouldBeNull();
        resultList.List.ShouldNotBeNull();
    }
}