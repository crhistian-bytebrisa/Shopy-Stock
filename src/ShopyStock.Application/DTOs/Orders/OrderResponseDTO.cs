using ShopyStock.Domain.Enum;

namespace ShopyStock.Application.DTOs;

public record OrderResponseDTO(
    int Id,
    DateTime Date,
    List<OrderDetailsProductDTO> Products,
    decimal TotalPrice,
    OrderStatus Status
);