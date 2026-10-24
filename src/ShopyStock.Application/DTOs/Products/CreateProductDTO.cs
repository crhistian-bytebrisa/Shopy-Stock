namespace ShopyStock.Application.DTOs;

public record CreateProductDTO(
    string Name,
    decimal Price,
    int Stock
);