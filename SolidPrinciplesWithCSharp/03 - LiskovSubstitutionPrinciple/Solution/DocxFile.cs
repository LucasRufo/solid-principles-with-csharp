namespace SolidPrinciplesWithCSharp.Solution;

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
