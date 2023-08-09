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

    [Fact]
    public void GetLoginUrl_Should_Do_As_Excepted()
    {
        string state = Guid.NewGuid().ToString();
        var result = _baseFixture.Client.GetLoginUrl(state);

        var uri = new Uri(result);
        uri.AbsoluteUri.Contains(BasecampApiClient.AuthUrl).ShouldBeTrue();
    }

    [Fact]
    public async Task RefreshToken_Should_Do_As_Excepted()
    {
        var result = await _baseFixture.Client.RefreshTokenAsync();
        result.ShouldNotBeNull();
    }
}