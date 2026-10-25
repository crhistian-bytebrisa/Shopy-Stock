using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopyStock.Domain.Entities;

namespace ShopyStock.Infraestructure.Context.SeedData;

public class OrderDetailsSeed : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.HasData(
            new OrderDetails()
            {
                ProductId = 4,
                OrderId = OrdersSeed.OrderId,
                Quantity = 2,
                UnitPrice = 5000
            }
        );
        
    }
}