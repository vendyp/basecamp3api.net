using Basecamp3Api.Models;

namespace Basecamp3Api.Tests.Projects;

[Collection(nameof(BaseFixture))]
public class GetAllProjectTests
{
    private readonly BaseFixture _baseFixture;

    public GetAllProjectTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetAllProject_Should_Do_As_Expected()
    {
        var auth = await _baseFixture.Client.GetAuthorizationAsync();
        auth.Auth.ShouldNotBeNull();

        var page = 1;
        var projects = await _baseFixture.Client.GetAllProjectAsync(auth.Auth.Accounts!.First().Id, 1);
        projects.List!.Results.Count.ShouldBePositive();

        if (projects.List.HasNextPage)
        {
            projects = await _baseFixture.Client.GetAllProjectAsync(auth.Auth.Accounts!.First().Id, ++page);
            projects.List!.Results.Count.ShouldBePositive();
        }
    }
}