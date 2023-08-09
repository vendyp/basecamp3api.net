namespace Basecamp3Api.Tests.Authorization;

[Collection(nameof(BaseFixture))]
public class AuthenticationTests
{
    private readonly BaseFixture _baseFixture;

    public AuthenticationTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAuthorization_Should_Do_As_Excepted()
    {
        var result = await _baseFixture.Client.GetAuthorizationAsync();
        result.ShouldNotBeNull();
    }
}