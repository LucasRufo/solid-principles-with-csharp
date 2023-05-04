namespace SolidPrinciplesWithCSharp.Violation;

public class TransactionValidator
{
    public bool Validate(decimal value)
    {
        if (value <= 0)
            return false;

        return true;
    }
}
