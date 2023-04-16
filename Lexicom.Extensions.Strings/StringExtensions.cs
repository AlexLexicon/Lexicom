namespace Lexicom.Extensions.Strings;
public static class StringExtensions
{
    public static string? ToNameCase(this string? str)
    {
        return CapitalizeFirstLetter(str?.ToLower());
    }

    public static string? CapitalizeFirstLetter(this string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return string.Concat(str[0].ToString().ToUpper(), str.AsSpan(1));
    }
}