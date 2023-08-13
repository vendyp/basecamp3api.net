namespace Basecamp3Api.Tests.Projects;

[Collection(nameof(Basecamp3ApiTestCollection))]
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
        var page = 1;
        var projects = await _baseFixture.Client.GetAllProjectAsync(_baseFixture.AccountId, 1);
        projects.List!.Results.Count.ShouldBePositive();

        if (projects.List.HasNextPage)
        {
            projects = await _baseFixture.Client.GetAllProjectAsync(_baseFixture.AccountId, ++page);
            projects.List!.Results.Count.ShouldBePositive();
        }
    }
}