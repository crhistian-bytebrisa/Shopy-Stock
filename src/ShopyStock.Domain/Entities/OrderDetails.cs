using System.ComponentModel.DataAnnotations;

namespace ShopyStock.Domain.Entities;

public class OrderDetails
{
    [Required(ErrorMessage = "Es necesario el ID del pedido.")]
    public Guid OrderId { get; set; }

    public Order? Order { get; set; } = null;
    
    [Required(ErrorMessage = "Es necesario el ID del producto.")]
    public int ProductId { get; set; }

    public Product? Product { get; set; } = null;
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
}