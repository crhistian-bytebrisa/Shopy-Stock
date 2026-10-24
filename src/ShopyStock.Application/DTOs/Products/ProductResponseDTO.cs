namespace ShopyStock.Application.DTOs;

public record ProductResponseDTO(
    int Id, 
    string Name, 
    decimal Price, 
    int Stock
);