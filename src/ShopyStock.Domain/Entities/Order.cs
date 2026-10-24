using System.ComponentModel.DataAnnotations;
using ShopyStock.Domain.Enum;

namespace ShopyStock.Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Se necesita ingresar el dia del pedido.")]
    public DateTime Date { get; set; }
    
    [Required(ErrorMessage = "Se necesita ingrear el total del pedido.")]
    public decimal TotalPrice { get; set; }

    [Required] 
    public OrderStatus Status { get; set; } = OrderStatus.Confirmed;
    
    //Navegation
    public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}