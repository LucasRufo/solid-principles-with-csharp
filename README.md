# Solid Principles With C#

This purpose of this content is to have a place to revisitate this concepts once in a while, but feel free to use it as a resource or send suggestions for improvements.

The SOLID principles were created as guidelines to write software that is easier to maintain. Adopting the SOLID principles can help developers avoid code smells and refactoring code. The principles were reunited by Robect C. Martin, but the acronym was nominated by Michael Feathers.

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

## Liskov Substitution Principle 

> "A program that uses an interface must not be confused by an implementation of that interface." 

> "If S is a subtype of T, then objects of type T in a program may be replaced with objects of type S without altering any of the desirable properties of that program."

To exemplify this principle, we have this code: 

```csharp
public class DocxFile
{
    public virtual void Update()
    {
        Console.WriteLine("Updating DOCX file...");
    }

    public virtual void Download()
    {
        Console.WriteLine("Downloading DOCX file...");
    }
}
```

```csharp
public class PdfFile : DocxFile
{
    public override void Update()
    {
        throw new InvalidOperationException();
    }

    public override void Download()
    {
        Console.WriteLine("Downloading PDF file...");
    }
}
```

As we can see, the class `PdfFile` can't be a substitute for the class `DocxFile` because it's not possible to update a PDF file, so if we call the `Update` method using the class `PdfFile` we would get an error. To fix this, we can refactor de code to this:

```csharp
public class File
{
    public virtual void Download()
    {
        Console.WriteLine("Downloading generic file...");
    }
}
```

```csharp
public class DocxFile : File
{
    public void Update()
    {
        Console.WriteLine("Updating DOCX file...");
    }

    public override void Download()
    {
        Console.WriteLine("Downloading DOCX file...");
    }
}
```

```csharp
public class PdfFile : File
{
    public override void Download()
    {
        Console.WriteLine("Downloading PDF file...");
    }
}
```

Now the `PdfFile` class can act as a `File` base class without problems. We are enforcing consistency so that our parent class and our child class can be used in the same way without errors.

## Interface Segregation Principle

> "Keep interfaces small so that users don’t end up depending on things they don’t need."

This one is very straight forward, looking at this code you should already see the principle violation:

```csharp
public interface IRepository
{
    public string Save(string name);
    public List<string> List();
    public string Update(string oldName, string newName);
}
```

```csharp
public class UserRepository : IRepository
{
    private List<string> _userList = new();
    public List<string> List()
    {
        return _userList;
    }

    public string Save(string name)
    {
        _userList.Add(name);
        return $"User {name} saved!";
    }

    public string Update(string oldName, string newName)
    {
        _userList.Remove(oldName);
        _userList.Add(newName);
        return $"User with old name {oldName} updated to {newName}!";
    }
}
```

```csharp
public class CityRepository : IRepository
{
    public List<string> List()
    {
        return new List<string>() { "São Paulo", "Rio de Janeiro", "Fortaleza" };
    }

    public string Save(string name)
    {
        throw new NotImplementedException("We don't have this operation for cities");
    }

    public string Update(string oldName, string newName)
    {
        throw new NotImplementedException("We don't have this operation for cities");
    }
}
```

The class `CityRepository` doesn't have Save and Update operations, it has a fixed list of cities that don't change. So our contract with the `IRepository` interface is violating the principle, because the `CityRepository` doesn't need those two extra methods, and this can lead to unexpected bugs.

We can break our interface in two different interfaces:

```csharp
public interface IReadRepository
{
    public List<string> List();
}

public interface IWriteRepository
{
    public string Save(string name);
    public string Update(string oldName, string newName);
}
```

So our code can implement only the interfaces that are really needed: 

```csharp
public class UserRepository : IReadRepository, IWriteRepository
{
    private List<string> _userList = new();
    public List<string> List()
    {
        return _userList;
    }

    public string Save(string name)
    {
        _userList.Add(name);
        return $"User {name} saved!";
    }

    public string Update(string oldName, string newName)
    {
        _userList.Remove(oldName);
        _userList.Add(newName);
        return $"User with old name {oldName} updated to {newName}!";
    }
}
```

```csharp
public class CityRepository : IReadRepository
{
    public List<string> List()
    {
        return new List<string>() { "São Paulo", "Rio de Janeiro", "Fortaleza" };
    }
}
```

Now our `CityRepository` only has the `List` method, and our code is respecting the principle.

## Dependency Inversion Principle

> "Depend in the direction of abstraction. High level modules should not depend upon low level details."

This principle says that we should depend on abstractions instead of implementations. Take this code as an example: 

```csharp
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
```

```csharp
public class TransactionValidator
{
    public bool Validate(decimal value)
    {
        if (value <= 0)
            return false;

        return true;
    }
}
```

```csharp
public class TransactionRepository
{
    public void Save(Guid userId, decimal value)
    {
        Console.WriteLine($"Saving transaction from user {userId} with value {value}");
    }
}
```

Our class `TransactionService` uses the implementation of two classes, a repository and a validator. If at some point we make changes in those dependencies, our `TransactionService` would need a change too, so we are violating the Dependency Inversion Principle.

To respect the principle, we can use dependency injection and interfaces to abstract the behavior that our service class needs: 

```csharp
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
```

```csharp
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
```

```csharp
public class TransactionRepository : ITransactionRepository
{
    public void Save(Guid userId, decimal value)
    {
        Console.WriteLine($"Saving transaction from user {userId} with value {value}");
    }
}

public interface ITransactionRepository
{
    void Save(Guid userId, decimal value);
}
```

Now our `TransactionService` only depends on interfaces and it only knows the contract of those dependencies.

## Resources

https://www.lambda3.com.br/2022/06/principios-solid-boas-praticas-de-programacao-com-c-parte-1-srp-single-responsability-principle/

https://www.lambda3.com.br/2022/07/principios-solid-boas-praticas-de-programacao-com-c-parte-2-ocp-open-closed-principle/

https://www.lambda3.com.br/2022/07/principios-solid-boas-praticas-de-programacao-com-c-parte-3-lsp-liskovs-substitution-principle/

https://www.lambda3.com.br/2022/07/principios-solid-boas-praticas-de-programacao-com-c-parte-4-isp-interface-segregation-principle/

https://www.lambda3.com.br/2022/07/principios-solid-boas-praticas-de-programacao-com-c-parte-5-dip-dependency-inversion-principle/

https://www.zup.com.br/blog/design-principle-solid

https://blog.cleancoder.com/uncle-bob/2020/10/18/Solid-Relevance.html

https://www.freecodecamp.org/news/solid-principles-explained-in-plain-english/

https://www.digitalocean.com/community/conceptual-articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design

https://medium.com/backticks-tildes/the-s-o-l-i-d-principles-in-pictures-b34ce2f1e898

https://methodpoet.com/liskov-substitution-principle/

https://medium.com/netcoders/aplicando-solid-com-c-isp-interface-segregation-principle-e6683f1d6975