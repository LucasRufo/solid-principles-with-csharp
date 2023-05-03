using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesWithCSharp.Violation;

public class Payment
{
    public void Process(decimal value, PaymentType type)
    {
        if (type == PaymentType.CreditCard)
            ProcessCreditCardPayement(value);
        if (type == PaymentType.DebitCard)
            ProcessDebitCardPayement(value);
        if (type == PaymentType.CreditCard)
            ProcessCashPayement(value);
    }

    public enum PaymentType
    {
        CreditCard = 0,
        DebitCard,
        Cash
    }

    private static void ProcessCreditCardPayement(decimal value) => Console.WriteLine($"Credit card payment: {value}");
    private static void ProcessDebitCardPayement(decimal value) => Console.WriteLine($"Debit card payment: {value}");
    private static void ProcessCashPayement(decimal value) => Console.WriteLine($"Cash payment: {value}");
}
