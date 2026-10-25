namespace ShopyStock.Application.DTOs.Request;

public record RequestOrder
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}