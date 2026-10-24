namespace ShopyStock.Application.DTOs;

public record OrderDetailsProductDTO(
    int ProductId,
    string NameProduct,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal
);