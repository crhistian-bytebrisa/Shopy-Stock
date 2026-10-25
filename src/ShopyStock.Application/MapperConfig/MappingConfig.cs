using Mapster;
using ShopyStock.Application.DTOs.Orders;
using ShopyStock.Application.DTOs.Products;
using ShopyStock.Domain.Entities;

namespace ShopyStock.Application.MapperConfig;

public class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Product, ProductDto>.NewConfig();
        TypeAdapterConfig<ProductDto, Product>.NewConfig();
        TypeAdapterConfig<CreateProductDto, Product>.NewConfig();
        TypeAdapterConfig<UpdateProductDto, Product>.NewConfig()
            .IgnoreNullValues(true);
        
        TypeAdapterConfig<Order, OrderDto>.NewConfig();
        TypeAdapterConfig<OrderDto, Order>.NewConfig();
        TypeAdapterConfig<CreateOrderDto, Order>.NewConfig()
            .Map(dest => dest.OrderDetails,
                src => src.Details);
        TypeAdapterConfig<Order, OrderResponseDto>.NewConfig()
            .Map(dest => dest.Products, src => src.OrderDetails);

        TypeAdapterConfig<OrderDetails, OrderDetailsProductDto>.NewConfig()
            .Map(dest => dest.NameProduct, src => src.Product != null ? src.Product.Name : "Nombre no registrado.")
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice)
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.Subtotal, src => src.Quantity * src.UnitPrice);

        TypeAdapterConfig<OrderDetailsProductDto, OrderDetails>.NewConfig();
        TypeAdapterConfig<CreateOrderDetialsDto, OrderDetails>.NewConfig()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.Quantity, src => src.Quantity);
        
    }
}