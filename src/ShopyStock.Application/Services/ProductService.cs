using Mapster;
using Microsoft.EntityFrameworkCore;
using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Products;
using ShopyStock.Application.Interfaces;
using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Interfaces.Repositories;

namespace ShopyStock.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<PagedResult<ProductResponseDto>> GetAll(RequestProduct request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(
            request.PageSize,
            request.PageIndex,
            request.Name,
            request.MinPrice,
            request.MaxPrice,
            cancellationToken
            );
        
        List<ProductResponseDto> list = products.Item1.Adapt<List<ProductResponseDto>>();

        return new PagedResult<ProductResponseDto>(request.PageIndex, request.PageSize, products.Item2, list);
    }

    public async Task<ProductResponseDto?> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        
        if (product == null) throw new KeyNotFoundException("No existe un Producto con ese identificador");
        
        return product.Adapt<ProductResponseDto>();
    }

    public async Task AddEntity(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            await _unitOfWork.Products.AddAsync(createProductDto.Adapt<Product>(),cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateEntity(int id, UpdateProductDto updateProductDto, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException("No se encontro el producto.");
        }
        
        updateProductDto.Adapt(product);
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteEntity(int id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException("No se encontro el producto.");
        }
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
}