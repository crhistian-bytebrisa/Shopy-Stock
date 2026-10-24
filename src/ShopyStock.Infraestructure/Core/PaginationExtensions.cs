using Microsoft.EntityFrameworkCore;

namespace ShopyStock.Infraestructure.Core;

public static class PaginationExtensions
{
    public static async Task<(List<T> Items, int TotalCount)> PaginateAsync<T>(
        this IQueryable<T> query,
        int PageIndex, int PageSize) where T : class
    {
        int totalCount = await query.CountAsync();

        List<T> list = await query
            .AsNoTracking()
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
        
        return (list, totalCount); 
    }
}