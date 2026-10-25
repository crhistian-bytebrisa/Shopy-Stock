using FluentValidation;
using Mapster;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Orders;
using ShopyStock.Application.DTOs.Request;
using ShopyStock.Application.Interfaces;
using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Exceptions;
using ShopyStock.Domain.Interfaces.Repositories;
using ValidationException = ShopyStock.Domain.Exceptions.ValidationException;

namespace ShopyStock.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateOrderDto> _validator;

    public OrderService(IUnitOfWork unitOfWork, IValidator<CreateOrderDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<PagedResult<OrderDto>> GetAll(RequestOrder request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetAllAsync(
            request.PageIndex,
            request.PageSize,
            request.StartDate,
            request.EndDate,
            cancellationToken
        );
        
        List<OrderDto> list = orders.Item1.Adapt<List<OrderDto>>();

        return new PagedResult<OrderDto>(request.PageIndex, request.PageSize, orders.Item2, list);
    }
    public async Task<OrderResponseDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
        
        if (order == null) throw new KeyNotFoundException("No se encontro la orden.");
        
        return order.Adapt<OrderResponseDto>();
    }
    public async Task AddEntity(CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        //Validaciones
        var validationResult = await _validator.ValidateAsync(createOrderDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ValidationException(errors);
        }
        
        
        var orderProducts = createOrderDto.Details
            .GroupBy(x => x.ProductId)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

        var productIds = orderProducts.Keys.ToList();
        var products = await _unitOfWork.Products.GetListProducts(productIds, cancellationToken);
        
        if (products.Count != productIds.Count)
        {
            throw new KeyNotFoundException("Selecciono productos que no estan registrados.");
        }

        var removeProducts = new List<Product>();

        foreach (var product in products)
        {
            var quantity = orderProducts[product.Id];
            if (quantity > product.Stock)
            {
                removeProducts.Add(product);
            }
        }

        if (removeProducts.Any())
        {
            throw new StockException("Algunos productos no estan disponibles.", removeProducts);
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var order = createOrderDto.Adapt<Order>();
            order.Id = Guid.NewGuid();
            order.Date = DateTime.UtcNow;

            decimal calculatedTotal = 0;
            
            foreach (var detail in order.OrderDetails) 
            {
                var product = products.First(p => p.Id == detail.ProductId);
                
                detail.OrderId = order.Id;
                detail.UnitPrice = product.Price;

                product.Stock -= detail.Quantity;
                _unitOfWork.Products.Update(product);

                calculatedTotal += product.Price * detail.Quantity;
            }

            order.TotalPrice = calculatedTotal;

            await _unitOfWork.Orders.AddAsync(order, cancellationToken);
            
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