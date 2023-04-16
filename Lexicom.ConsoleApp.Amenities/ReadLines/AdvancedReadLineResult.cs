namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineResult
{
    public AdvancedReadLineResult(
        bool isContinue,
        string? input)
    {
        IsContinue = isContinue;
        Input = input;
    }

    public bool IsContinue { get; }
    public string? Input { get; }
}
