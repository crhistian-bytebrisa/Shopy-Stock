using ShopyStock.Domain.Entities;

namespace ShopyStock.Domain.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<(IEnumerable<Product>, int totalCount)> GetAllAsync(
        int pagesize,
        int pageindex,
        string? name,
        decimal? minprice,
        decimal? maxprice,
        CancellationToken cancellationToken = default);
    
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}