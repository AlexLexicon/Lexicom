using FluentAssertions;
using Xunit;

namespace Lexicom.Maths.UnitTests;
public class MathxTests
{
    [Theory]
    [InlineData(0, 1, 0)]
    [InlineData(0, 5, 0)]
    [InlineData(0, 10, 0)]
    [InlineData(0, 16, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(1, 5, 11111)]
    [InlineData(1, 10, 1111111111)]
    [InlineData(1, 15, 111111111111111)]
    [InlineData(2, 1, 2)]
    [InlineData(2, 5, 22222)]
    [InlineData(2, 10, 2222222222)]
    [InlineData(2, 15, 222222222222222)]
    [InlineData(3, 1, 3)]
    [InlineData(3, 5, 33333)]
    [InlineData(3, 10, 3333333333)]
    [InlineData(3, 15, 333333333333333)]
    [InlineData(4, 1, 4)]
    [InlineData(4, 5, 44444)]
    [InlineData(4, 10, 4444444444)]
    [InlineData(4, 15, 444444444444444)]
    [InlineData(5, 1, 5)]
    [InlineData(5, 5, 55555)]
    [InlineData(5, 10, 5555555555)]
    [InlineData(5, 15, 555555555555555)]
    [InlineData(6, 1, 6)]
    [InlineData(6, 5, 66666)]
    [InlineData(6, 10, 6666666666)]
    [InlineData(6, 15, 666666666666666)]
    [InlineData(7, 1, 7)]
    [InlineData(7, 5, 77777)]
    [InlineData(7, 10, 7777777777)]
    [InlineData(7, 15, 777777777777777)]
    [InlineData(8, 1, 8)]
    [InlineData(8, 5, 88888)]
    [InlineData(8, 10, 8888888888)]
    [InlineData(8, 15, 888888888888888)]
    [InlineData(9, 1, 9)]
    [InlineData(9, 5, 99999)]
    [InlineData(9, 10, 9999999999)]
    [InlineData(9, 15, 999999999999999)]
    public void CreateIntegerOfRepeatedDigitsNTimes_CreatesIntegerOfCorrectValue(int digit, int n, long expected)
    {
        long result = Mathx.CreateIntegerOfRepeatedDigitsNTimes(digit, n);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(10)]
    [InlineData(int.MaxValue)]
    public void CreateIntegerOfRepeatedDigitsNTimes_Throws_ArgumentOutOfRangeException_When_Digit_Is_Out_Of_Range(int digit)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Mathx.CreateIntegerOfRepeatedDigitsNTimes(digit, 5);
        });

        exception.ActualValue.Should().Be(digit);
        exception.ParamName.Should().Be("digit");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(16)]
    [InlineData(int.MaxValue)]
    public void CreateIntegerOfRepeatedDigitsNTimes_Throws_ArgumentOutOfRangeException_When_n_Is_Out_Of_Range(int n)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Mathx.CreateIntegerOfRepeatedDigitsNTimes(5, n);
        });

        exception.ActualValue.Should().Be(n);
        exception.ParamName.Should().Be("n");
    }
}