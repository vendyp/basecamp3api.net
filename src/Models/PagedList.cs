namespace Basecamp3Api.Models;

public class PagedList<T>
{
    public PagedList(List<T> results)
    {
        Results = results;
    }

    public List<T> Results { get; set; }
    public bool HasNextPage { get; set; }
}