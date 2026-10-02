using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;

internal class AdvancedReadLineInterrupt(ConsoleKey? interceptKey) : AdvancedReadLineIntercept(interceptKey)
{
    public bool IsInterrupted { get; protected set; }

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        IsInterrupted = true;

        return new AdvancedReadLineResult(isContinue: false, currentInput);
    }

    public override AdvancedReadLineInitialResult Initial()
    {
        return new AdvancedReadLineInitialResult(IsInitial: false, input: null);
    }
}
