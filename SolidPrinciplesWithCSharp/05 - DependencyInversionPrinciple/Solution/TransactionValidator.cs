namespace SolidPrinciplesWithCSharp.Dip.Solution;

public class TransactionValidator : ITransactionValidator
{
    public bool Validate(decimal value)
    {
        if (value <= 0)
            return false;

        return true;
    }
}

public interface ITransactionValidator
{
    bool Validate(decimal value);
}
