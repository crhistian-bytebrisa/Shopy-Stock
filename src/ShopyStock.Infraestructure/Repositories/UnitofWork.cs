using System.Data;
using Mapster;
using ShopyStock.Domain.Interfaces.Repositories;
using ShopyStock.Infraestructure.Context;

namespace ShopyStock.Infraestructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public readonly StockDbContext _dbContext;
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(StockDbContext stockDbContext, IProductRepository productRepository,  IOrderRepository orderRepository)
    {
        _dbContext = stockDbContext;
        Products = productRepository;
        Orders = orderRepository;
    }
    
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await  _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}