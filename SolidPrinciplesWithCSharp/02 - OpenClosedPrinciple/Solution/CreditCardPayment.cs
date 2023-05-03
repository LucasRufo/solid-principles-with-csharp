namespace SolidPrinciplesWithCSharp.Solution;

public class CreditCardPayment : IPayment
{
    public void Process(decimal value)
    {
        Console.WriteLine($"Credit card payment: {value}");
    }
}
