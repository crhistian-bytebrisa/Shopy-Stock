using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Products;

namespace ShopyStock.Application.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductResponseDto>> GetAll(RequestProduct request, CancellationToken cancellationToken);
    Task<ProductResponseDto?> GetById(int id, CancellationToken cancellationToken);
    Task AddEntity(CreateProductDto createProductDto, CancellationToken cancellationToken);
    Task UpdateEntity(int id, UpdateProductDto updateProductDTO, CancellationToken cancellationToken);  
    Task DeleteEntity(int id, CancellationToken cancellationToken);
}