namespace SolidPrinciplesWithCSharp.Violation;

public class Transaction
{
    public decimal Value { get; set; }
    public decimal ValueWithTax { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    private int Tax { get; set; } = 10;

    public Transaction(decimal value, string from, string to)
    {
        Value = value;
        From = from;
        To = to;
        ValueWithTax = CalculateTransactionTax();
    }

    private decimal CalculateTransactionTax()
    {
        return Value * Tax;
    }

    public void SaveTransaction()
    {
        string newTransaction = $"{Value},{ValueWithTax},{From},{To},{Environment.NewLine}";
        File.WriteAllText("database.txt", newTransaction);
    }

    public void LogTransactionToConsole()
    {
        Console.WriteLine($"{Value},{ValueWithTax},{From},{To},{Environment.NewLine}");
    }
}
