namespace SolidPrinciplesWithCSharp.Solution;

public class CashPayment : IPayment
{
    public void Process(decimal value)
    {
        Console.WriteLine($"Cash payment: {value}");
    }
}
