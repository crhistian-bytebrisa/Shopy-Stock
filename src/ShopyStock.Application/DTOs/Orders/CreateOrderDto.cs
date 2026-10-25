using ShopyStock.Domain.Enum;

namespace ShopyStock.Application.DTOs.Orders;

public class CreateOrderDto(){
    public List<CreateOrderDetialsDto> Details { get; set; } = new();
    public OrderStatus Status { get; set; }
};