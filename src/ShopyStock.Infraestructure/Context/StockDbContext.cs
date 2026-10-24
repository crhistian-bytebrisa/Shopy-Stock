using Microsoft.EntityFrameworkCore;
using ShopyStock.Domain.Entities;

namespace ShopyStock.Infraestructure.Context;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    

    public DbSet<Product> Products { get; set; }   
    public DbSet<Order> Orders { get; set; }
}