using ShopyStock.Domain.Enum;

namespace ShopyStock.Application.DTOs.Orders;

public class OrderResponseDto()
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public List<OrderDetailsProductDto> Products { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
}