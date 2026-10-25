namespace ShopyStock.Application.DTOs.Products;

public record CreateProductDto(
    string Name,
    decimal Price,
    int Stock
);