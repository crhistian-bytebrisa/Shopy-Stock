namespace ShopyStock.Application.DTOs;

public record ProductDTO(
    int Id,
    string Name, 
    decimal Price
);