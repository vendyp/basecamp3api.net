﻿namespace Basecamp3Api;

public partial class BasecampApiClient
{
    public async Task<(People? People, Error? Error)> GetMyPersonalInfoAsync(
        long accountId,
        CancellationToken cancellationToken)
    {
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
        var pattern = $"{accountId}/my/profile.json";
        var uri = new Uri(BaseUrl + pattern);

        var request = CreateRequestMessageWithAuthentication(HttpMethod.Get, uri, null);

        var response = await SendMessageAsync(request, HttpStatusCode.OK, cancellationToken);

        if (response.Error != null)
            return (null, response.Error);

        var result = JsonSerializer.Deserialize<People>(response.Response!.Value.ResultJsonInString)!;

        return (result, null);
    }
}