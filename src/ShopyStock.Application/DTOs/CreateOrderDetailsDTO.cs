namespace ShopyStock.Application.DTOs;

public record CreateOrderDetialsDTO(
    Guid OrderId,
    int ProductId,
    decimal UnitPrice,
    int Quantity
);