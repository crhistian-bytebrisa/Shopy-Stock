using ShopyStock.Domain.Entities;

namespace ShopyStock.Domain.Exceptions;

public class StockException : Exception
{
    public List<Product> RejectedProducts { get; }

    public StockException(string message, List<Product> rejectedProducts) : base(message)
    {
        RejectedProducts = rejectedProducts;
    }
}