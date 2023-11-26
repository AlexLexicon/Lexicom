using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineDefault(ConsoleKey? interceptKey, string? defaultInput) : AdvancedReadLineIntercept(interceptKey)
{
    public string? DefaultInput { get; } = defaultInput;

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        return new AdvancedReadLineResult(isContinue: true, DefaultInput);
    }

    public override AdvancedReadLineInitalResult Initial()
    {
        return new AdvancedReadLineInitalResult(IsInital: false, input: null);
    }
}