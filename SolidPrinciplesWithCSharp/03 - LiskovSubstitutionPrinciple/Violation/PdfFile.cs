namespace SolidPrinciplesWithCSharp.Violation;

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
