namespace Basecamp3Api.Models;

public record UpdateProject 
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Admissions { get; set; }
    public DateTime? StartDt { get; set; }
    public DateTime? EndDt { get; set; }
}