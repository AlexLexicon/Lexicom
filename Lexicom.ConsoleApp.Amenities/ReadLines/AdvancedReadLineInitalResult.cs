namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineInitalResult
{
    public AdvancedReadLineInitalResult(
        bool IsInital,
        string? input)
    {
        this.IsInital = IsInital;
        Input = input;
    }

    public bool IsInital { get; }
    public string? Input { get; }
}
