namespace SolidPrinciplesWithCSharp.Solution;

public interface IWriteRepository
{
    public string Save(string name);
    public string Update(string oldName, string newName);
}
