namespace SolidPrinciplesWithCSharp.Violation;

public interface IRepository
{
    public string Save(string name);
    public List<string> List();
    public string Update(string oldName, string newName);
}
