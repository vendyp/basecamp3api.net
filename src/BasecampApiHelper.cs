using System.Collections.Specialized;

namespace Basecamp3Api;

public partial class BasecampApiClient
{
    /// <summary>
    /// Helper method for creating HttpRequestMessage
    /// </summary>
    /// <param name="httpMethod"></param>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    internal HttpRequestMessage CreateRequestMessage(HttpMethod httpMethod, Uri uri, HttpContent? content)
    {
        var request = new HttpRequestMessage();
        request.RequestUri = uri;
        if (httpMethod == HttpMethod.Get)
            return request;
        if (content != null)
            request.Content = content;

        return request;
    }

    /// <summary>
    /// Helper method for creating HttpRequestMessage with Bearer Authentication
    /// </summary>
    /// <param name="httpMethod"></param>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    internal HttpRequestMessage CreateRequestMessageWithAuthentication(HttpMethod httpMethod, Uri uri,
        HttpContent? content)
    {
        var request = new HttpRequestMessage();
        request.Method = httpMethod;
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
        request.Headers.Add("User-Agent", $"{_setting.AppName} ({_setting.RedirectUrl})");
        request.RequestUri = uri;
        if (httpMethod == HttpMethod.Get)
            return request;
        if (content != null)
            request.Content = content;

        return request;
    }

    /// <summary>
    /// Helper method for construct query string based on <see cref="NameValueCollection"/>
    /// </summary>  
    /// <param name="nvc"></param>
    /// <returns></returns>
    internal string ConstructQueryString(NameValueCollection nvc)
    {
        var array = (
            from key in nvc.AllKeys
            from value in nvc.GetValues(key)!
            select string.Format(
                "{0}={1}",
                HttpUtility.UrlEncode(key),
                HttpUtility.UrlEncode(value))
        ).ToArray();
        return "?" + string.Join("&", array);
    }

    internal async Task<(Response? Response, Error? Error)> SendMessageAsync(HttpRequestMessage message,
        HttpStatusCode successStatusCode,
        CancellationToken cancellationToken)
    {
        var maxRetry = 3;
        var interval = 1;
        while (interval <= maxRetry)
        {
            var response = await _httpClient.SendAsync(message, cancellationToken);
            var contentAsString = await response.Content.ReadAsStringAsync(cancellationToken);
            switch (response.StatusCode)
            {
                //handle too many requests
                case HttpStatusCode.TooManyRequests:
                    response.Headers.TryGetValues("Retry-After", out var retryAfterHeaders);
                    var waitIntervalInSeconds = 2;
                    var afterHeaders = retryAfterHeaders as string[] ?? retryAfterHeaders?.ToArray();
                    if (afterHeaders != null && afterHeaders.Length > 0)
                    {
                        var value = afterHeaders.First();
                        waitIntervalInSeconds = Convert.ToInt32(value);
                    }

                    await Task.Delay(waitIntervalInSeconds * 1000, cancellationToken);
                    interval++;
                    continue;

                //handle not found
                case HttpStatusCode.NotFound:
                    response.Headers.TryGetValues("Reason", out var reasonHeaders);
                    var reasons = reasonHeaders as string[] ?? reasonHeaders?.ToArray();
                    if (reasons == null || reasons.Length < 1)
                        return (null, new Error
                        {
                            Message = "Data not found",
                            StatusCode = 404
                        });

                    var s = reasons.First();
                    if (!string.IsNullOrWhiteSpace(s))
                        return (null, new Error
                        {
                            Message = "Account Inactive",
                            StatusCode = 404
                        });

                    return (null, new Error
                    {
                        Message = "Data not found",
                        StatusCode = 404
                    });

                //handle insufficient storage
                case HttpStatusCode.InsufficientStorage:
                    return (null, new Error
                    {
                        Message = "The project limit for this account has been reached",
                        StatusCode = 507
                    });

                //handle others
                default:
                    if (response.StatusCode != successStatusCode)
                        return (null,
                            new Error
                            {
                                Message = $"Invalid request with message : {contentAsString}", StatusCode = 500
                            });

                    var resp = new Response
                    {
                        ResultJsonInString = contentAsString
                    };

                    var hasHeaderLink = response.Headers.TryGetValues("Link", out var links);
                    if (hasHeaderLink)
                    {
                        resp.Headers.Add("Link", links!.First());
                    }

                    return (resp, null);
            }
        }

        return (null, new Error { Message = "Max retry exceed", StatusCode = 500 });
    }
}

internal record struct Response()
{
    public string ResultJsonInString { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new();
}