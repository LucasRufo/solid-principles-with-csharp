namespace SolidPrinciplesWithCSharp.Violation;

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
