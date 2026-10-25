namespace ShopyStock.Application.DTOs.Orders;

public class CreateOrderDetialsDto()
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}