using ShopyStock.Domain.Enum;

namespace ShopyStock.Application.DTOs;

public record OrderDTO(
    int Id, 
    DateTime Date,
    decimal TotalPrice, 
    OrderStatus Status
);