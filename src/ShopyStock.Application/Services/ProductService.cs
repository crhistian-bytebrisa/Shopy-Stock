using Mapster;
using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.Interfaces;
using ShopyStock.Domain.Interfaces.Repositories;

namespace ShopyStock.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<PagedResult<ProductResponseDTO>> GetAll(RequestProduct request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(
            request.PageSize,
            request.PageIndex,
            request.Name,
            request.MinPrice,
            request.MaxPrice,
            cancellationToken
            );
        
        List<ProductResponseDTO> list = products.Item1.Adapt<List<ProductResponseDTO>>();

        return new PagedResult<ProductResponseDTO>(request.PageIndex, request.PageSize, products.Item2, list);
    }

    public async Task<ProductResponseDTO> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        return product.Adapt<ProductResponseDTO>();
    }

    public async Task AddEntity(CreateProductDTO CProductDTO, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateEntity(UpdateProductDTO updateProductDTO, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEntity(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}