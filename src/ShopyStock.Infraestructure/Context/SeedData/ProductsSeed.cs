using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopyStock.Domain.Entities;

namespace ShopyStock.Infraestructure.Context.SeedData;

public class ProductsSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop Lenovo P14S 2da Gen",
                Price = 35000,
                Stock = 10
            },
            new Product
            {
                Id = 2,
                Name = "Mouse Logitech G Pro",
                Price = 6700,
                Stock = 15
            },
            new Product
            {
                Id = 3,
                Name = "Teclado Mecanico Krom Kasic TKL",
                Price = 16000,
                Stock = 15
            }
        );
    }
}