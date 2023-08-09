using Basecamp3Api.Exceptions;
using Basecamp3Api.Models;

namespace Basecamp3Api.Tests.Projects;

[Collection(nameof(BaseFixture))]
public class CreateProjectTests
{
    private readonly BaseFixture _baseFixture;

    public CreateProjectTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task CreateProject_Should_Do_As_Excepted()
    {
        var auth = await _baseFixture.Client.GetAuthorizationAsync();
        auth.ShouldNotBeNull();

        await Assert.ThrowsAsync<InsufficientStorageException>(async () =>
        {
            await _baseFixture.Client.CreateProjectAsync(auth.Accounts!.Last().Id, new CreateProject
            {
                Name = "Test Project"
            });
        });
    }
}