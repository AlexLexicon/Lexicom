using Lexicom.Extensions.Strings;

namespace UnitTests.For.Lexicom.Extensions.Strings.IntegerExtensionsTests;

public class GetCountWordTextTests
{
    [Theory]
    [InlineData(0, "zero")]
    [InlineData(1, "one")]
    [InlineData(2, "two")]
    [InlineData(3, "three")]
    [InlineData(4, "four")]
    [InlineData(5, "five")]
    [InlineData(6, "six")]
    [InlineData(7, "seven")]
    [InlineData(8, "eight")]
    [InlineData(9, "nine")]
    [InlineData(10, "ten")]
    [InlineData(11, "many")]
    [InlineData(123, "many")]
    [InlineData(999, "many")]
    [InlineData(-1, "less than zero")]
    [InlineData(-2, "less than zero")]
    [InlineData(-3, "less than zero")]
    [InlineData(-4, "less than zero")]
    public void Test(int number, string expectedValue)
    {
        string actualValue = number.GetSimpleNumberText();

        Assert.Equal(expectedValue, actualValue);
    }
}
