using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Interfaces.Repositories;

namespace ShopyStock.Infraestructure.Repositories;

public class ProductRepository : IProductRepository
{

    public ProductRepository()
    {
    }
    public Task<Product> AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Product> DeleteAsync(Product entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync(int pagesize, int pageindex, string? name, decimal? minprice, decimal? maxprice,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}