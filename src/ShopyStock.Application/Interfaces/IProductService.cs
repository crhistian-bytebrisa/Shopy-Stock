using ShopyStock.Application.DTOs;

namespace ShopyStock.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDTO>> GetAll(RequestProduct request, CancellationToken cancellationToken);
    Task<ProductResponseDTO> GetById(int id, CancellationToken cancellationToken);
    Task Create(CreateProductDTO CProductDTO , CancellationToken cancellationToken);
    Task Update();
}