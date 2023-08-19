namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(ResponseWhoCanAccessProject? Response, Error? Error)> UpdateWhoCanAccessProject(
        long accountId,
        long projectId,
        UpdateWhoCanAccessProjectOptions? options,
        CancellationToken cancellationToken)
    {
        #region Validation

        if (options is null)
            return (null, new Error
            {
                Message = "Options parameter can not be null"
            });

        if (options.Creates is null && options.Grants is null && options.Revokes is null)
            return (null, new Error
            {
                Message = "At least one of them value is not null"
            });

        bool atLeastOne = false;

        if (options.Creates != null && options.Creates.Any())
        {
            atLeastOne = true;

            foreach (var item in options.Creates)
            {
                var error = ValidateGrantUserOption(item);
                if (error != null)
                    return (null, error);
            }
        }

        if (options.Grants != null && options.Grants.Any())
            atLeastOne = true;

        if (options.Revokes != null && options.Revokes.Any())
            atLeastOne = true;

        if (!atLeastOne)
            return (null, new Error
            {
                Message = "At least one of them value is not null"
            });

        #endregion

        if (!TokenHasBeenSet)
            return (null, new Error
            {
                StatusCode = -1,
                Message = "Token has not been set"
            });

        var err = ValidateAccount(accountId);
        if (err != null)
            return (null, err);

        //$ACCOUNT_ID/projects/.json
        var pattern = $"{accountId}/projects/{projectId}/people/users.json";
        var uri = new Uri(BaseUrl + pattern);

        var json = JsonSerializer.Serialize(options);

        var request = CreateRequestMessageWithAuthentication(
            HttpMethod.Put,
            uri,
            new StringContent(json, Encoding.UTF8, "application/json"));

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        var result =
            JsonSerializer.Deserialize<ResponseWhoCanAccessProject>(response.Response!.Value.ResultJsonInString)!;

        return (result, null);
    }

    private Error? ValidateGrantUserOption(AddNewUserToProject project)
    {
        if (string.IsNullOrWhiteSpace(project.Name))
            return new Error
            {
                Message = "Name can not be null"
            };

        if (string.IsNullOrWhiteSpace(project.EmailAddress))
            return new Error
            {
                Message = "Email address can not be null"
            };

        if (!ValidEmailAddress(project.EmailAddress))
            return new Error
            {
                Message = $"{project.EmailAddress} is not a valid email address"
            };

        return null;
    }

    private bool ValidEmailAddress(string s)
    {
        var trimmedEmail = s.Trim();

        if (trimmedEmail.EndsWith("."))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(s);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}

public record UpdateWhoCanAccessProjectOptions
{
    [JsonPropertyName("grant")] public List<long>? Grants { get; set; } = new();
    [JsonPropertyName("revoke")] public List<long>? Revokes { get; set; } = new();
    [JsonPropertyName("create")] public List<AddNewUserToProject>? Creates { get; set; } = new();
}

public record AddNewUserToProject
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("email_address")] public string EmailAddress { get; set; } = null!;
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("company_name")] public string? CompanyName { get; set; }
}

public record ResponseWhoCanAccessProject
{
    [JsonPropertyName("granted")] public List<People>? Granted { get; set; } = new();
    [JsonPropertyName("revoked")] public List<People>? Revoked { get; set; } = new();
}