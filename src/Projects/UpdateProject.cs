namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task UpdateProjectAsync(int accountId, long projectId, UpdateProject data,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            throw new InvalidOperationException("Token has not been set");

        if (!AccountHasBeenSet)
            throw new InvalidOperationException("Account has not been set");

        if (!Accounts.Any(e => e.Id == accountId))
            throw new ArgumentException("Invalid account id", nameof(accountId));

        if (string.IsNullOrWhiteSpace(data.Name))
            throw new InvalidValidationException("Property name of UpdateProject can not be null or empty");

        if (data.StartDt.HasValue || data.EndDt.HasValue)
        {
            if (data.StartDt.HasValue && data.EndDt.HasValue == false)
                throw new InvalidValidationException("Property EndDt in UpdateProject can not be null");

            if (data.EndDt.HasValue && data.StartDt.HasValue == false)
                throw new InvalidValidationException("Property StartDt in UpdateProject can not be null");

            if (data.StartDt.HasValue && data.EndDt.HasValue && data.StartDt.Value > data.EndDt.Value)
                throw new InvalidValidationException("StartDt must less than EndDt");
        }

        //$ACCOUNT_ID/projects/{projectId}.json
        var pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new UriBuilder(BaseUrl + pattern);

        var request = new HttpRequestMessage(HttpMethod.Put, uri.ToString());
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        request.Headers.Add("User-Agent", $"Basecamp 4 Library ({_setting.RedirectUrl})");
        request.Content = new StringContent(JsonSerializer.Serialize(new
        {
            name = data.Name,
            description = data.Description,
            admissions = data.Admissions,
            schedule_attributes = new
            {
                start_date = data.StartDt?.ToString("yyyy-MM-dd"),
                end_date = data.EndDt?.ToString("yyyy-MM-dd")
            }
        }, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Result not Created when Update project", new Exception($"With message : {content}"));

        throw new NotImplementedException();
    }
}