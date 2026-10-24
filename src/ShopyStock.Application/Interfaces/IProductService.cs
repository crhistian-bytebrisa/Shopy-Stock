using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;

namespace ShopyStock.Application.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductResponseDTO>> GetAll(RequestProduct request, CancellationToken cancellationToken);
    Task<ProductResponseDTO> GetById(int id, CancellationToken cancellationToken);
    Task AddEntity(CreateProductDTO createProductDto, CancellationToken cancellationToken);
    Task UpdateEntity(UpdateProductDTO updateProductDTO, CancellationToken cancellationToken);  
    Task DeleteEntity(int id, CancellationToken cancellationToken);
}