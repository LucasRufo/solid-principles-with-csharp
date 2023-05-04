namespace SolidPrinciplesWithCSharp.Violation;

public class TransactionRepository
{
    public void Save(Guid userId, decimal value)
    {
        Console.WriteLine($"Saving transaction from user {userId} with value {value}");
    }
}
