using FluentValidation;
using Mapster;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Products;
using ShopyStock.Application.DTOs.Request;
using ShopyStock.Application.Interfaces;
using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Interfaces.Repositories;
using ValidationException = ShopyStock.Domain.Exceptions.ValidationException;

namespace ShopyStock.Application.Services;

public class ProductService : IProductService
{
    private readonly IValidator<CreateProductDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork, IValidator<CreateProductDto> validator)
    {
        _validator = validator;
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
        
        //Validacion
        if (product == null) throw new KeyNotFoundException("No se encontro el producto.");
        
        return product.Adapt<ProductResponseDto>();
    }

    public async Task AddEntity(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        //Validaciones
        var validationResult = await _validator.ValidateAsync(createProductDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ValidationException(errors);
        }
        
        var exists = await _unitOfWork.Products.Exists(createProductDto.Name, cancellationToken);

        if (exists == true) throw new InvalidOperationException("Ya existe un producto con ese nombre.");
        
        
        //Ejecucion
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
        //Validaciones
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException("No se encontro el producto.");
        }
        
        updateProductDto.Adapt(product);
        
        
        //Ejecucion
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
        //Validaciones
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException("No se encontro el producto.");
        }

        var ordered = await _unitOfWork.Products.ProductOrdered(id, cancellationToken);

        if (ordered == true)
        {
            throw new InvalidOperationException("No se puede eliminar el producto porque ya esta asociado a uno o mas pedidos.");
        }
        
        //Ejecucion
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