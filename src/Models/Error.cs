namespace Basecamp3Api.Models;

public record struct Error
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}