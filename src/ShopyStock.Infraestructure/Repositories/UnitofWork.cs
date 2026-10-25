using System.Data;
using Mapster;
using Microsoft.EntityFrameworkCore.Storage;
using ShopyStock.Domain.Interfaces.Repositories;
using ShopyStock.Infraestructure.Context;

namespace ShopyStock.Infraestructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public readonly StockDbContext _dbContext;
    private IDbContextTransaction? _actualTransaction;
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
        if (_actualTransaction != null) return;

        _actualTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_actualTransaction == null) 
            throw new InvalidOperationException("No hay una transacción activa para hacer commit.");

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken); 
            await _actualTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_actualTransaction == null) return;

        try
        {
            await _actualTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task DisposeTransactionAsync()
    {
        if (_actualTransaction != null)
        {
            await _actualTransaction.DisposeAsync();
            _actualTransaction = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeTransactionAsync();
        await _dbContext.DisposeAsync();
    }
}