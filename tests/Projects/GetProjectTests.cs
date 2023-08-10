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
        auth.ShouldNotBeNull();

        var projects = await _baseFixture.Client.GetAllProjectAsync(auth.Accounts!.First().Id, 1);
        projects.Results.Count.ShouldBePositive();

        var project = await _baseFixture.Client.GetProjectAsync(auth.Accounts!.First().Id, projects.Results.First().Id);
        project.ShouldNotBeNull();
        projects.Results.First().Id.ShouldBe(project.Id);
    }
}