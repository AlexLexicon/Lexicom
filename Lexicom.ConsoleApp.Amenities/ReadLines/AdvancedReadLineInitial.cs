using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;

internal class AdvancedReadLineInitial(string? initialInput) : AdvancedReadLineIntercept(null)
{
    public string? InitialInput { get; } = initialInput;

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        return new AdvancedReadLineResult(isContinue: true, input: null);
    }

    public override AdvancedReadLineInitialResult Initial()
    {
        return new AdvancedReadLineInitialResult(IsInitial: true, InitialInput);
    }
}