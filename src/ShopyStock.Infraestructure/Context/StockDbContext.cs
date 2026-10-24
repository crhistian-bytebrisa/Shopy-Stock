using Microsoft.EntityFrameworkCore;
using ShopyStock.Domain.Entities;

namespace ShopyStock.Infraestructure.Context;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        modelBuilder.Entity<OrderDetails>()
            .HasKey(od => new { od.OrderId, od.ProductId });

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<OrderDetails>()
            .Property(o => o.UnitPrice)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(18, 2);
        
        base.OnModelCreating(modelBuilder);
    }
    

    public DbSet<Product> Products { get; set; }   
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
}