using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Extensions;
using NSubstitute;

namespace Lexicom.ConsoleApp.Amenities.UnitTests.Tests.ConsolexTests;

public class ReadLineDateTimeOffsetTests
{
    [Fact]
    public void TEst()
    {
        var uta = new UnitTestAssistant();

        var console = uta.Mock<IConsolexConsole>().So(c =>
        {
            var x = Test("2026-05-18 15:29:32");

            c.ReadKey(Arg.Any<bool>()).ReturnsThese(x);
            c.BufferWidth.Returns(x.Count);
        }).Pull();

        Consolex.ConsolexConsole = console;

        DateTimeOffset dt = Consolex.ReadLineDateTimeOffset();
    }

    private List<ConsoleKeyInfo> Test(string text)
    {
        var x = new List<ConsoleKeyInfo>();
        foreach (char character in text)
        {
            x.Add(new ConsoleKeyInfo(character, ConsoleKey.None, shift: false, alt: false, control: false));
        }

        x.Add(new ConsoleKeyInfo(new char(), ConsoleKey.Enter, shift: false, alt: false, control: false));

        return x;
    }
}
