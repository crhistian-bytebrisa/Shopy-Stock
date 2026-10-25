namespace ShopyStock.Application.DTOs.Orders;
public class OrderDetailsProductDto()
{
    public int ProductId { get; set; }
    public string NameProduct { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}