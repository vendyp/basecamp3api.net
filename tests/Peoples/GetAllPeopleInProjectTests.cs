namespace Basecamp3Api.Tests.Peoples;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetAllPeopleInProjectTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllPeopleInProjectTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAllPeopleInProject_Should_Do_As_Expected()
    {
        var todolists = await _baseFixture.Client.GetAllPeopleInProjectAsync(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            CancellationToken.None);

        todolists.Error.ShouldBeNull();
        todolists.Peoples.ShouldNotBeNull();
    }
}