using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Enum;

namespace ShopyStock.Infraestructure.Context.SeedData;

public class OrdersSeed : IEntityTypeConfiguration<Order>
{
    public static Guid OrderId = Guid.Parse("51449123-eede-4982-aa0c-7a618346b174");
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasData(
            new Order()
            {
                Date = DateTime.UtcNow,
                Id = OrderId,
                Status = OrderStatus.Confirmed,
                TotalPrice = 10000
            } 
        );
        
    }
}