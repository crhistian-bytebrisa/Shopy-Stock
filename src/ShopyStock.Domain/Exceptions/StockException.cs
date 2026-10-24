namespace ShopyStock.Domain.Exceptions;

public class StockException : Exception
{
    public StockException(string message) : base(message)
    {
        
    }
}