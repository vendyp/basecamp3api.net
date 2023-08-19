namespace Basecamp3Api.Tests.Peoples;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class UpdateWhoCanAccessProjectTests
{
    private readonly BaseFixture _baseFixture;

    public UpdateWhoCanAccessProjectTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    public static IEnumerable<object[]> GetInvalidRequests()
    {
        yield return new object[]
        {
            new UpdateWhoCanAccessProjectOptions
            {
                Grants = null,
                Revokes = null,
                Creates = null
            }
        };

        yield return new object[]
        {
            null!
        };

        yield return new object[]
        {
            new UpdateWhoCanAccessProjectOptions()
        };

        yield return new object[]
        {
            new UpdateWhoCanAccessProjectOptions
            {
                Creates = new List<AddNewUserToProject>
                {
                    new()
                }
            }
        };

        yield return new object[]
        {
            new UpdateWhoCanAccessProjectOptions
            {
                Creates = new List<AddNewUserToProject>
                {
                    new()
                    {
                        Name = "Test"
                    }
                }
            }
        };

        yield return new object[]
        {
            new UpdateWhoCanAccessProjectOptions
            {
                Creates = new List<AddNewUserToProject>
                {
                    new()
                    {
                        EmailAddress = "aaa"
                    }
                }
            }
        };

        yield return new object[]
        {
            new UpdateWhoCanAccessProjectOptions
            {
                Creates = new List<AddNewUserToProject>
                {
                    new()
                    {
                        Name = "Test Name",
                        EmailAddress = "aaa"
                    }
                }
            }
        };
    }

    [Theory]
    [MemberData(nameof(GetInvalidRequests))]
    public async Task UpdateWhoCanAccessProjectTests_Given_Invalid_Option_Should_Return_Error(
        UpdateWhoCanAccessProjectOptions? options)
    {
        var result = await _baseFixture.Client.UpdateWhoCanAccessProject(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            options,
            CancellationToken.None);

        result.Response.ShouldBeNull();
        result.Error.ShouldNotBeNull();
    }

    [Fact(Skip = "Already tested, I did skip this because could spam the recipient's email")]
    public async Task UpdateWhoCanAccessProject_Should_Do_As_Expected()
    {
        var options = new UpdateWhoCanAccessProjectOptions
        {
            Creates = new List<AddNewUserToProject>
            {
                new()
                {
                    Name = "***",
                    EmailAddress = "lorep@gmail.com"
                }
            }
        };

        var result = await _baseFixture.Client.UpdateWhoCanAccessProject(
            _baseFixture.AccountId,
            _baseFixture.ProjectId,
            options,
            CancellationToken.None);

        result.Response.ShouldNotBeNull();
    }
}