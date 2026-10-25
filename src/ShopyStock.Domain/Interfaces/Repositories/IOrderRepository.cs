using ShopyStock.Domain.Entities;

namespace ShopyStock.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<(IEnumerable<Order>, int totalCount)> GetAllAsync(
        int pagesize,
        int pageindex,
        DateTime? startdate,
        DateTime? enddate,
        CancellationToken cancellationToken = default);
    
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Order entity, CancellationToken cancellationToken = default);
    void Update(Order entity);
    void Delete(Order entity);
}