using Lexicom.ConsoleApp.Amenities;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Extensions;
using NSubstitute;

namespace UnitTests.For.Lexicom.ConsoleApp.Amenities.ConsolexTests;

public class ReadLineDateTimeOffsetTests
{
    [Fact]
    public void ReadLineDateTimeOffset_Can_Read_DateTime()
    {
        var uta = new UnitTestAssistant();

        var console = uta.Mock<IConsolexConsole>().So(c =>
        {
            var consoleKeyInfos = StringToConsoleInput("2026-05-18 15:29:32");

            c.ReadKey(Arg.Any<bool>()).ReturnsThese(consoleKeyInfos);
            c.BufferWidth.Returns(consoleKeyInfos.Count);
        }).Pull();

        Consolex.ConsolexConsole = console;

        DateTimeOffset dt = Consolex.ReadLineDateTimeOffset();
    }

    private List<ConsoleKeyInfo> StringToConsoleInput(string text)
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
