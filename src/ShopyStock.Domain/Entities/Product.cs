using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ShopyStock.Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre debe ser ingresado al producto.")]
    [MinLength(4, ErrorMessage = "El nombre debe tener al menos 4 caracteres.")]
    public string Name { get; set; }
    
    [Required]
    public decimal Price { get; set; }

    [Required] 
    public int Stock { get; set; } = 0;
    
    //Navegation 
    public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}