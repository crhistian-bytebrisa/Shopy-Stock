using ShopyStock.Domain.Entities;

namespace ShopyStock.Domain.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<(IEnumerable<Order>, int totalCount)> GetAllAsync(
        int pagesize,
        int pageindex,
        DateTime? startdate,
        DateTime? enddate,
        CancellationToken cancellationToken = default);
    
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}