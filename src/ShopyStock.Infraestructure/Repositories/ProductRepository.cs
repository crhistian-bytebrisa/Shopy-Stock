using Microsoft.EntityFrameworkCore;
using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Interfaces.Repositories;
using ShopyStock.Infraestructure.Context;
using ShopyStock.Infraestructure.Core;

namespace ShopyStock.Infraestructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StockDbContext _context;

    public ProductRepository(StockDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(entity, cancellationToken);
    }

    public void Update(Product entity)
    {
        _context.Update(entity);
    }

    public void Delete(Product entity)
    {
        _context.Remove(entity);
    }

    public async Task<(IEnumerable<Product>, int totalCount)> GetAllAsync(int pagesize, int pageindex, string? name, decimal? minprice, decimal? maxprice,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();
        if (name != null)
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        if (minprice != null)
        {
            query = query.Where(x => x.Price >= minprice);
        }

        if (maxprice != null)
        {
            query = query.Where(x => x.Price <= maxprice);
        }

        var result = await query.PaginateAsync(pageindex, pagesize);
        return result;
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}