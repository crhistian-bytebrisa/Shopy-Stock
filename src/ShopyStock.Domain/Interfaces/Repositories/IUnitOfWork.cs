using System.Data;

namespace ShopyStock.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}