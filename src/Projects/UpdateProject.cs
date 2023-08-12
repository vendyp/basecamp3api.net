namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<Error?> UpdateProjectAsync(
        long accountId,
        long projectId,
        UpdateProject data,
        CancellationToken cancellationToken = default)
    {
        if (!TokenHasBeenSet)
            return new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            };

        var err = ValidateAccount(accountId);
        if (err != null)
            return err;

        if (string.IsNullOrWhiteSpace(data.Name))
            return new Error
            {
                StatusCode = -1,
                Message = "Property name of UpdateProject can not be null or empty"
            };

        if (data.StartDt.HasValue || data.EndDt.HasValue)
        {
            if (data.StartDt.HasValue && data.EndDt.HasValue == false)
                return new Error
                {
                    StatusCode = -1,
                    Message = "Property EndDt in UpdateProject can not be null"
                };

            if (data.EndDt.HasValue && data.StartDt.HasValue == false)
                return new Error
                {
                    StatusCode = -1,
                    Message = "Property StartDt in UpdateProject can not be null"
                };

            if (data.StartDt.HasValue && data.EndDt.HasValue && data.StartDt.Value > data.EndDt.Value)
                return new Error
                {
                    StatusCode = -1,
                    Message = "StartDt must less than EndDt"
                };
        }

        //$ACCOUNT_ID/projects/{projectId}.json
        var pattern = $"{accountId}/projects/{projectId}.json";
        var uri = new Uri(BaseUrl + pattern);

        var content = new StringContent(JsonSerializer.Serialize(new
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

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Put, uri, content);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        return response.Error;
    }
}