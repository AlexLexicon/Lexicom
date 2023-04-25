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
        return new AdvancedReadLineResult(isContinue: true, DefaultInput);
    }

    public override AdvancedReadLineInitalResult Initial()
    {
        return new AdvancedReadLineInitalResult(IsInital: false, input: null);
    }
}