using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Interfaces.Repositories;

namespace ShopyStock.Infraestructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<Order> AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order> DeleteAsync(Order entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync(int pagesize, int pageindex, DateTime? startdate, DateTime? enddate,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}