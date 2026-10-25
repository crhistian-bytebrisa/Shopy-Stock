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
            query = query.Where(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
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

    public async Task<List<Product>> GetListProducts(List<int> ids, CancellationToken cancellationToken = default)
    {
        if (ids == null || !ids.Any())
        {
            throw new KeyNotFoundException("Debes ingresar los productos.");
        }

        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ProductOrdered(int id, CancellationToken cancellationToken)
    {
        return await _context.OrderDetails
            .AnyAsync(x => x.ProductId == id, cancellationToken);
    }
    
    public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> Exists(string name, CancellationToken cancellationToken)
    {
        name = name.Trim();

        return await _context.Products
            .AnyAsync(x => x.Name.Trim() == name, cancellationToken);
    }
}