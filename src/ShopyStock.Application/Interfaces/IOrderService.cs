using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;

namespace ShopyStock.Application.Interfaces;

public interface IOrderService 
{
    Task<PagedResult<OrderDTO>> GetAll(RequestProduct request, CancellationToken cancellationToken);
    Task<OrderResponseDTO> GetById(int id, CancellationToken cancellationToken);
    Task AddEntity(CreateOrderDTO createOrderDto, CancellationToken cancellationToken);
}