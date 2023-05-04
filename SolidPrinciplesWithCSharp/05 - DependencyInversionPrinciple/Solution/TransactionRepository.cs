namespace SolidPrinciplesWithCSharp.Dip.Solution;

public class TransactionRepository : ITransactionRepository
{
    public void Save(Guid userId, decimal value)
    {
        Console.WriteLine($"Saving transaction from user {userId} with value {value}");
    }
}

public interface ITransactionRepository
{
    void Save(Guid userId, decimal value);
}

