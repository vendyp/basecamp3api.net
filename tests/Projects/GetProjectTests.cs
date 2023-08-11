using Basecamp3Api.Models;

namespace Basecamp3Api.Tests.Projects;

[Collection(nameof(BaseFixture))]
public class GetProjectTests
{
    private readonly BaseFixture _baseFixture;

    public GetProjectTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetProject_Should_Do_As_Expected()
    {
        var auth = await _baseFixture.Client.GetAuthorizationAsync();
        auth.Auth.ShouldNotBeNull();

        var projects = await _baseFixture.Client.GetAllProjectAsync(auth.Auth.Accounts!.First().Id, 1);
        projects.List!.Results.Count.ShouldBePositive();

        var project =
            await _baseFixture.Client.GetProjectAsync(auth.Auth.Accounts!.First().Id, projects.List.Results.First().Id);
        project.Error.ShouldBeNull();
        project.Project.ShouldNotBeNull();
        projects.List.Results.First().Id.ShouldBe(project.Project.Id);
    }
}