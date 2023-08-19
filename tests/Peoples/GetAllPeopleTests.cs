namespace Basecamp3Api.Tests.Peoples;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetAllPeopleTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllPeopleTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAllPeople_Should_Do_As_Expected()
    {
        var results = await _baseFixture.Client.GetAllPeopleAsync(
            _baseFixture.AccountId,
            CancellationToken.None);

        results.Error.ShouldBeNull();
        results.Peoples.ShouldNotBeNull();
    }
}