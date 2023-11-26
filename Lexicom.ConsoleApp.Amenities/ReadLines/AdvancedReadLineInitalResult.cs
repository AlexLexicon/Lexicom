namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineInitalResult(bool IsInital, string? input)
{
    public bool IsInital { get; } = IsInital;
    public string? Input { get; } = input;
}
