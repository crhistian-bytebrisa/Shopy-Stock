using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Orders;
using ShopyStock.Application.DTOs.Request;

namespace ShopyStock.Application.Interfaces;

public interface IOrderService 
{
    Task<PagedResult<OrderDto>> GetAll(RequestOrder request, CancellationToken cancellationToken);
    Task<OrderResponseDto?> GetById(Guid id, CancellationToken cancellationToken);
    Task AddEntity(CreateOrderDto createOrderDto, CancellationToken cancellationToken);
}