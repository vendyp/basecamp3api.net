using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Web;
using Basecamp3Api;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Welcome to basecamp 4 api client console app");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var setting = new BasecampApiSetting
{
    ClientSecret = configuration.GetValue<string>("ClientSecretId"),
    ClientId = configuration.GetValue<string>("ClientId"),
    RedirectUrl = new Uri(configuration.GetValue<string>("RedirectUrl")!),
    AppName = configuration.GetValue<string>("AppName")!,
};

var client = new BasecampApiClient(setting);

var uriBuilder = new UriBuilder(BasecampApiClient.AuthUrl);
var query = HttpUtility.ParseQueryString(uriBuilder.Query);
query["type"] = "web_server";
query["client_id"] = setting.ClientId;
query["redirect_uri"] = setting.RedirectUrl.OriginalString;
uriBuilder.Query = query.ToString();

var url = uriBuilder.ToString();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}"));
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    Process.Start("xdg-open", "'" + url + "'");
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    Process.Start("open", "'" + url + "'");
}
else
{
    throw new ApplicationException("Unknown OS platform");
}

Console.Write("Please input code : ");

string? code = Console.ReadLine();
if (string.IsNullOrWhiteSpace(code))
{
    Console.WriteLine("Invalid login");
}

HttpClient httpClient = new HttpClient();


// POST https://launchpad.37signals.com/authorization/token?type=web_server&client_id=your-client-id&redirect_uri=your-redirect-uri&client_secret=your-client-secret&code=verification-code
var tokenUriBuilder = new UriBuilder(BasecampApiClient.AuthTokenUrl);
query = HttpUtility.ParseQueryString(tokenUriBuilder.Query);
query["type"] = "web_server";
query["client_id"] = setting.ClientId;
query["redirect_uri"] = setting.RedirectUrl.OriginalString;
query["client_secret"] = setting.ClientSecret;
query["code"] = code;
tokenUriBuilder.Query = query.ToString();
var request = new HttpRequestMessage(HttpMethod.Post, tokenUriBuilder.ToString());

var response = await httpClient.SendAsync(request, CancellationToken.None);
var content = await response.Content.ReadAsStringAsync();

await using FileStream createStream = File.Create(@"..\tests\basecamp.json");
await JsonSerializer.SerializeAsync(createStream, JsonSerializer.Deserialize<object>(content), new JsonSerializerOptions
{
    WriteIndented = true
});

Console.WriteLine("Successfully, write file to folder root\\tests\\basecamp.json");