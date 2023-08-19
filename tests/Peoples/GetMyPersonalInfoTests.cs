namespace Basecamp3Api.Tests.Peoples;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetMyPersonalInfoTests
{
    private readonly BaseFixture _baseFixture;

    public GetMyPersonalInfoTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetMyPersonalInfo_Should_Do_As_Expected()
    {
        var result = await _baseFixture.Client.GetMyPersonalInfoAsync(
            _baseFixture.AccountId,
            CancellationToken.None);

        result.Error.ShouldBeNull();
        result.People.ShouldNotBeNull();
    }
}