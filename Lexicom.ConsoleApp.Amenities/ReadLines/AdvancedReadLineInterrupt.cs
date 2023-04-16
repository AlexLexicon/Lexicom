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

        return new AdvancedReadLineResult(false, currentInput);
    }
}
