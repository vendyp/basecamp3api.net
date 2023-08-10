namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public Task UpdateProjectAsync(int accountId, UpdateProject data, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(data.Name))
            throw new InvalidValidationException();

        if (data.StartDt.HasValue || data.EndDt.HasValue)
            if (data.StartDt == null || data.EndDt == null)
                throw new InvalidValidationException();

        throw new NotImplementedException();
    }
}