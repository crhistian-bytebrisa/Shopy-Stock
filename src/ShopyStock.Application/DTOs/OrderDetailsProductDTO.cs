namespace ShopyStock.Application.DTOs;

public record OrderDetailsProductDTO(
    string NameProduct,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal
);