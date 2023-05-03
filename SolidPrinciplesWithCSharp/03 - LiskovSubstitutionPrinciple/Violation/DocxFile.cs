namespace SolidPrinciplesWithCSharp.Violation;

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
