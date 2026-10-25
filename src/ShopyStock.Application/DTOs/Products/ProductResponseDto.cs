namespace ShopyStock.Application.DTOs.Products;

public record ProductResponseDto(
    int Id, 
    string Name, 
    decimal Price, 
    int Stock
);