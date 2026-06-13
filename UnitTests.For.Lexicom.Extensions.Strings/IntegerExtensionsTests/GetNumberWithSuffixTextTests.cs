using Lexicom.Extensions.Strings;

namespace UnitTests.For.Lexicom.Extensions.Strings.IntegerExtensionsTests;

public class GetNumberWithSuffixTextTests
{
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1st")]
    [InlineData(2, "2nd")]
    [InlineData(3, "3rd")]
    [InlineData(4, "4th")]
    [InlineData(5, "5th")]
    [InlineData(6, "6th")]
    [InlineData(7, "7th")]
    [InlineData(8, "8th")]
    [InlineData(9, "9th")]
    [InlineData(10, "10th")]
    [InlineData(101, "101st")]
    [InlineData(222, "222nd")]
    [InlineData(123, "123rd")]
    [InlineData(999, "999th")]
    [InlineData(-1, "-1st")]
    [InlineData(-2, "-2nd")]
    [InlineData(-3, "-3rd")]
    [InlineData(-4, "-4th")]
    public void Text_Is_Correct(int number, string expectedValue)
    {
        string actualValue = number.GetNumberWithSuffixText();

        Assert.Equal(expectedValue, actualValue);
    }
}
