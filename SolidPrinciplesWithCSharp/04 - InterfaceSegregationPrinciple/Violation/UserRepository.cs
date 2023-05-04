namespace SolidPrinciplesWithCSharp.Violation;

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
