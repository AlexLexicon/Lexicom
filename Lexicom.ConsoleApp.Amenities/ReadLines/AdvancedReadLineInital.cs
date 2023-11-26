using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineInital(string? initalInput) : AdvancedReadLineIntercept(null)
{
    public string? InitalInput { get; } = initalInput;

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        return new AdvancedReadLineResult(isContinue: true, input: null);
    }

    public override AdvancedReadLineInitalResult Initial()
    {
        return new AdvancedReadLineInitalResult(IsInital: true, InitalInput);
    }
}