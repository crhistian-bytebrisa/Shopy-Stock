using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopyStock.Domain.Entities;

public class OrderDetails
{
    [Key]
    [Required(ErrorMessage = "Es necesario el ID del pedido.")]
    public Guid OrderId { get; set; }

    public Order? Order { get; set; } = null;
    
    [Key]
    [Required(ErrorMessage = "Es necesario el ID del producto.")]
    public int ProductId { get; set; }

    public Product? Product { get; set; } = null;
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
}