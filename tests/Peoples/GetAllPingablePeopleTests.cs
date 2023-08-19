namespace Basecamp3Api.Tests.Peoples;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetAllPingablePeopleTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllPingablePeopleTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetPingablePeople_Should_Do_As_Expected()
    {
        var results = await _baseFixture.Client.GetAllPingablePeopleAsync(
            _baseFixture.AccountId,
            CancellationToken.None);

        results.Error.ShouldBeNull();
        results.Peoples.ShouldNotBeNull();
    }
}