namespace SolidPrinciplesWithCSharp.Solution;

public class TransactionLogger
{
    public void LogTransactionToConsole(Transaction transaction)
    {
        Console.WriteLine($"{transaction.Value},{transaction.ValueWithTax},{transaction.From},{transaction.To},{Environment.NewLine}");
    }
}
