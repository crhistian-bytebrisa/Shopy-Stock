using ShopyStock.Domain.Enum;

namespace ShopyStock.Application.DTOs.Orders;

public record OrderDto(
    Guid Id, 
    DateTime Date,
    decimal TotalPrice, 
    OrderStatus Status
);