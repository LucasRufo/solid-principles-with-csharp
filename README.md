# Solid Principles With C#

This purpose of this content is to have a place to revisitate this concepts once in a while, but feel free to use it as a resource or send suggestions for improvements.

The SOLID principles were created as guidelines to write software that is easier to maintain. Adopting the SOLID principles can help developers to avoid code smells and refactoring code. The principles were reunited by Robect C. Martin, but the acronym was nominated by Michael Feathers.

All the code used in the examples are in a .NET project in this repo. 

Almost all the principle's definitions are from an [article](https://blog.cleancoder.com/uncle-bob/2020/10/18/Solid-Relevance.html) from Robect C. Martin.

1. [Single Responsibility Principle](#single-responsibility-principle)
2. [Open-Closed Principle](#open-closed-principle)
3. [Liskov Substitution Principle](#liskov-substitution-principle)
4. [Interface Segregation Principle](#interface-segregation-principle)
5. [Dependency Inversion Principle](#dependency-inversion-principle)

## Single Responsibility Principle

> "The Single Responsibility Principle states that a class should do one thing and therefore it should have only a single reason to change."

For example, take a look at this class: 

```csharp
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
```

The `Transaction` class has three functions: 

- The first one calculate the value from a field called `ValueWithTax`.
- The second one save the transaction to a file.
- The third one log all the data to the console.

Basically, our `Transaction` class has three different responsibilities, therefore it has three reasons to change. If at some point we want to change our code to save the transaction in a real database, we would change the `Transaction` class and we would have a chance to introduce bugs into code that was already working for the other two responsibilities. 

If we refactor this code into a version that respects the Single Responsibility Principle, it should look like this:

```csharp
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
}
```

```csharp
public class TransactionRepository
{
    public void SaveTransaction(Transaction transaction)
    {
        string newTransaction = $"{transaction.Value},{transaction.ValueWithTax},{transaction.From},{transaction.To},{Environment.NewLine}";
        File.WriteAllText("database.txt", newTransaction);
    }
}
```

```csharp
public class TransactionLogger
{
    public void LogTransactionToConsole(Transaction transaction)
    {
        Console.WriteLine($"{transaction.Value},{transaction.ValueWithTax},{transaction.From},{transaction.To},{Environment.NewLine}");
    }
}
```

Right now we have three different classes, and every class has its own responsibility, so if we want to change the way that we save our data to save to a different source like a database, we only modify the `TransactionRepository` class.

## Open Closed Principle

> "A Module should be open for extension but closed for modification."

What being open for extension but closed for modification means? Take a look at this class: 

```csharp
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
}
```

If at some point our financial department say that we need to accept a new payment type, we would modify the `Process` method and add one more condition. So our class is open to modifications in this state, and violates the first SOLID principle, because our `Process` method has more than one responsibility. 

We can refactor this code to something like this: 

```csharp
public interface IPayment
{
    public void Process(decimal value);
}
```

```csharp
public class CreditCardPayment : IPayment
{
    public void Process(decimal value)
    {
        Console.WriteLine($"Credit card payment: {value}");
    }
}
```

```csharp
public class DebitCardPayment : IPayment
{
    public void Process(decimal value)
    {
        Console.WriteLine($"Debit card payment: {value}");
    }
}
```

```csharp
public class CashPayment : IPayment
{
    public void Process(decimal value)
    {
        Console.WriteLine($"Cash payment: {value}");
    }
}
```

Now if a new payment type is needed, we only have to create a new class specifying the new type and extend the `IPayment` interface, we don't change any of our other implementations and now our classes have only one resposibility, respecting the first two principles. 

## Resources

https://www.lambda3.com.br/2022/06/principios-solid-boas-praticas-de-programacao-com-c-parte-1-srp-single-responsability-principle/

https://www.lambda3.com.br/2022/07/principios-solid-boas-praticas-de-programacao-com-c-parte-2-ocp-open-closed-principle/

https://www.zup.com.br/blog/design-principle-solid

https://blog.cleancoder.com/uncle-bob/2020/10/18/Solid-Relevance.html

https://www.freecodecamp.org/news/solid-principles-explained-in-plain-english/

https://www.digitalocean.com/community/conceptual-articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design

https://medium.com/backticks-tildes/the-s-o-l-i-d-principles-in-pictures-b34ce2f1e898