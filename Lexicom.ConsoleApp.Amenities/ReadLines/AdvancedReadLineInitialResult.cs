namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineInitialResult(bool IsInitial, string? input)
{
    public bool IsInitial { get; } = IsInitial;
    public string? Input { get; } = input;
}
