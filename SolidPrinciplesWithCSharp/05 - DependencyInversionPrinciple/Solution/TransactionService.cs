namespace SolidPrinciplesWithCSharp.Dip.Solution;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionValidator _transactionValidator;
    public TransactionService(ITransactionValidator validator, ITransactionRepository repository)
    {
        _transactionValidator = validator;
        _transactionRepository = repository;
    }

    public void ProcessTransaction(Guid userId, decimal value)
    {
        var isValid = _transactionValidator.Validate(value);

        if (!isValid)
            return;

        _transactionRepository.Save(userId, value);
    }
}
