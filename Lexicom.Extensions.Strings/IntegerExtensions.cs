namespace Lexicom.Extensions.Strings;

public static class IntegerExtensions
{
    public static string GetNumberWithSuffixText(this int number)
    {
        int lastDigit = number % 10;
        int lastTwoDigits = number % 100;

        if (number is 0)
        {
            return number.ToString();
        }

        if (lastDigit is 1 or -1 && lastTwoDigits is not 11 and not -11)
        {
            return $"{number}st";
        }

        if (lastDigit is 2 or -2 && lastTwoDigits is not 12 and not -12)
        {
            return $"{number}nd";
        }

        if (lastDigit is 3 or -3 && lastTwoDigits is not 13 and not -13)
        {
            return $"{number}rd";
        }

        return $"{number}th";
    }

    public static string GetSimpleNumberText(this int number, string moreText = "many", string lessText = "less than zero")
    {
        ArgumentNullException.ThrowIfNull(moreText);
        ArgumentNullException.ThrowIfNull(lessText);

        return number switch
        {
            0 => "zero",
            1 => "one",
            2 => "two",
            3 => "three",
            4 => "four",
            5 => "five",
            6 => "six",
            7 => "seven",
            8 => "eight",
            9 => "nine",
            10 => "ten",
            _ => number is < 0 ? lessText : moreText,
        };
    }
}