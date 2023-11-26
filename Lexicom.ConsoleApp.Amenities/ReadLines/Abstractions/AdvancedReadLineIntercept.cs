namespace Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;
internal abstract class AdvancedReadLineIntercept(ConsoleKey? interceptKey)
{
    public ConsoleKey? InterceptKey { get; } = interceptKey;

    public abstract AdvancedReadLineResult Intercept(string? currentInput);
    public abstract AdvancedReadLineInitalResult Initial();
}
