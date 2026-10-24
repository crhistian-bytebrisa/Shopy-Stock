namespace ShopyStock.Application.DTOs;

public class UpdateProductDTO
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
}