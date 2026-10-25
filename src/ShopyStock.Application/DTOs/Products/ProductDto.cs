namespace ShopyStock.Application.DTOs.Products;

public record ProductDto(
    int Id,
    string Name, 
    decimal Price
);