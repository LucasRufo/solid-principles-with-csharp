namespace SolidPrinciplesWithCSharp.Solution;

public class TransactionRepository
{
    public void SaveTransaction(Transaction transaction)
    {
        string newTransaction = $"{transaction.Value},{transaction.ValueWithTax},{transaction.From},{transaction.To},{Environment.NewLine}";
        File.WriteAllText("database.txt", newTransaction);
    }
}
