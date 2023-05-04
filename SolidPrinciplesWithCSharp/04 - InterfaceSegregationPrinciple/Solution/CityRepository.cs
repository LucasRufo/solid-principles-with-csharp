namespace SolidPrinciplesWithCSharp.Solution;

public class CityRepository : IReadRepository
{
    public List<string> List()
    {
        return new List<string>() { "São Paulo", "Rio de Janeiro", "Fortaleza" };
    }
}
