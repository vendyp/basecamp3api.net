namespace Basecamp3Api.Tests.Peoples;

[Collection(nameof(Basecamp3ApiTestCollection))]
public class GetPeopleTests
{
    private readonly BaseFixture _baseFixture;

    public GetPeopleTests(BaseFixture baseFixture)
    {
        _baseFixture = baseFixture;
    }

    [Fact]
    public async Task GetPeople_Should_Do_As_Expected()
    {
        var results = await _baseFixture.Client.GetAllPeopleAsync(
            _baseFixture.AccountId,
            CancellationToken.None);

        results.Error.ShouldBeNull();
        results.Peoples.ShouldNotBeNull();


        var first = results.Peoples.FirstOrDefault();
        if (first is null)
            return;

        var resultPeople = await _baseFixture.Client.GetPeopleAsync(
            _baseFixture.AccountId,
            first.Id,
            CancellationToken.None);

        resultPeople.Error.ShouldBeNull();
        resultPeople.People.ShouldNotBeNull();
        resultPeople.People.Id.ShouldBe(first.Id);
    }
}