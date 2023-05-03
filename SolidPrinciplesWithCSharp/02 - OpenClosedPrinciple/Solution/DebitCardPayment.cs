namespace SolidPrinciplesWithCSharp.Solution;

public class DebitCardPayment : IPayment
{
    public void Process(decimal value)
    {
        Console.WriteLine($"Debit card payment: {value}");
    }
}
