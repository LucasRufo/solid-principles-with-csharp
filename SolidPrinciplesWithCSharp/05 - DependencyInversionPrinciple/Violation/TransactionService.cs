namespace SolidPrinciplesWithCSharp.Violation;

public class TransactionService
{
    public void ProcessTransaction(Guid userId, decimal value)
    {
        var validator = new TransactionValidator();
        var repository = new TransactionRepository();

        var isValid = validator.Validate(value);

        if (!isValid)
            return;

        repository.Save(userId, value);
    }
}
