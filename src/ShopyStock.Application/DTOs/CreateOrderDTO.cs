using ShopyStock.Domain.Enum;

namespace ShopyStock.Application.DTOs;

public record CreateOrderDTO(
    DateTime Date,
    List<CreateOrderDetialsDTO> Details,
    decimal TotalPrice,
    OrderStatus Status
);