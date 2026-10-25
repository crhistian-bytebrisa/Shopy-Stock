using ShopyStock.Domain.Entities;

namespace ShopyStock.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<(IEnumerable<Product>, int totalCount)> GetAllAsync(
        int pagesize,
        int pageindex,
        string? name,
        decimal? minprice,
        decimal? maxprice,
        CancellationToken cancellationToken = default);
    
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetListProducts(List<int> ids, CancellationToken cancellationToken = default);
    Task<bool> ProductOrdered(int id, CancellationToken cancellationToken);
    Task<bool> Exists(int id, CancellationToken cancellationToken = default);
    Task<bool> Exists(string name, CancellationToken cancellationToken);
    Task AddAsync(Product entity, CancellationToken cancellationToken = default);
    void Update(Product entity);
    void Delete(Product entity);
}