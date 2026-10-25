using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Products;
using ShopyStock.Application.DTOs.Request;

namespace ShopyStock.Application.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductResponseDto>> GetAll(RequestProduct request, CancellationToken cancellationToken);
    Task<ProductResponseDto?> GetById(int id, CancellationToken cancellationToken);
    Task AddEntity(CreateProductDto createProductDto, CancellationToken cancellationToken);
    Task UpdateEntity(int id, UpdateProductDto updateProductDto, CancellationToken cancellationToken);  
    Task DeleteEntity(int id, CancellationToken cancellationToken);
}