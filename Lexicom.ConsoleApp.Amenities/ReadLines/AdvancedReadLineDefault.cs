using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineDefault : AdvancedReadLineIntercept
{
    public AdvancedReadLineDefault(
        ConsoleKey? interceptKey,
        string? defaultInput) : base(interceptKey)
    {
        DefaultInput = defaultInput;
    }

    public string? DefaultInput { get; }

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        return new AdvancedReadLineResult(true, DefaultInput);
    }
}