using Microsoft.EntityFrameworkCore;
using ShopyStock.Domain.Entities;
using ShopyStock.Domain.Interfaces.Repositories;
using ShopyStock.Infraestructure.Context;
using ShopyStock.Infraestructure.Core;

namespace ShopyStock.Infraestructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StockDbContext _context;

    public OrderRepository(StockDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(entity, cancellationToken);
    }

    public void Update(Order entity)
    {
        _context.Orders.Update(entity);
    }

    public void Delete(Order entity)
    {
        _context.Orders.Remove(entity);
    }

    public async Task<(IEnumerable<Order>, int totalCount)> GetAllAsync(int pageindex, int pagesize, DateTime? startdate, DateTime? enddate,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Orders.AsQueryable();

        if (startdate != null)
        {
            query.Where(x => x.Date >= startdate);
        }

        if (enddate != null)
        {
            query.Where(x => x.Date <= enddate);
        }

        var result = await query.PaginateAsync(pageindex, pagesize);
        return result;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(x => x.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}