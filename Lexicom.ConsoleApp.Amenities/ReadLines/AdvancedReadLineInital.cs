using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal class AdvancedReadLineInital : AdvancedReadLineIntercept
{
    public AdvancedReadLineInital(string? initalInput) : base(null)
    {
        InitalInput = initalInput;
    }

    public string? InitalInput { get; }

    public override AdvancedReadLineResult Intercept(string? currentInput)
    {
        return new AdvancedReadLineResult(isContinue: true, input: null);
    }

    public override AdvancedReadLineInitalResult Initial()
    {
        return new AdvancedReadLineInitalResult(IsInital: true, InitalInput);
    }
}