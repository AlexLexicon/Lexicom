namespace Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;
internal abstract class AdvancedReadLineIntercept
{
    public AdvancedReadLineIntercept(ConsoleKey? interceptKey)
    {
        InterceptKey = interceptKey;
    }

    public ConsoleKey? InterceptKey { get; }

    public abstract AdvancedReadLineResult Intercept(string? currentInput);
    public abstract AdvancedReadLineInitalResult Initial();
}
