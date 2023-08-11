namespace Basecamp3Api;

internal static class Extensions
{
    internal static UriBuilder AddQueryParams(this UriBuilder uriBuilder, string s)
    {
        uriBuilder.Query = s;
        return uriBuilder;
    }
}