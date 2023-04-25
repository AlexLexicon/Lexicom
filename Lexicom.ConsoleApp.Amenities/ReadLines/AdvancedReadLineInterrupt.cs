using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineInterrupt : AdvancedReadLineIntercept
{
    public AdvancedReadLineInterrupt(ConsoleKey? interceptKey) : base(interceptKey)
    {
    }

    public bool IsInterrupted { get; protected set; }

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        IsInterrupted = true;

        return new AdvancedReadLineResult(isContinue: false, currentInput);
    }

    public override AdvancedReadLineInitalResult Initial()
    {
        return new AdvancedReadLineInitalResult(IsInital: false, input: null);
    }
}
