namespace ShopyStock.Application.DTOs.Common;

public record PagedResult<T> 
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; }
    public int TotalItems { get; set; }

    public PagedResult(int page, int pageSize, int totalItems, List<T> items)
    {
        Page = page;
        PageSize = pageSize;
        Items = items;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
    }
}