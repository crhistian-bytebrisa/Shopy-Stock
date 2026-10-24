namespace ShopyStock.Application.DTOs;

public record OrderDetailsDTO(
    Guid OrderId,
    int ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal
);