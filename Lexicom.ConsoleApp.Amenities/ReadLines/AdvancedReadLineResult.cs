namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineResult(bool isContinue, string? input)
{
    public bool IsContinue { get; } = isContinue;
    public string? Input { get; } = input;
}
