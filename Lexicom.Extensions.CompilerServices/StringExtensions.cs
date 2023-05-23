namespace Lexicom.Extensions.CompilerServices;
public static class StringExtensions
{
    private const string UNKNOWN = "Unknown";
    private const string LAMBDA = "=>";
    private const int MAX_LENGTH = 256;

    /// <exception cref="ArgumentNullException"/>
    public static string SimplifyCallerArgumentExpression(this string callerArgumentExpression)
    {
        ArgumentNullException.ThrowIfNull(callerArgumentExpression);

        string[] lines = callerArgumentExpression.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (!lines.Any())
        {
            return UNKNOWN;
        }

        string flat = string.Join(" ", lines);

        if (string.IsNullOrWhiteSpace(flat))
        {
            return UNKNOWN;
        }

        int lambdaIndex = flat.IndexOf(LAMBDA);

        if (lambdaIndex < 0)
        {
            return flat;
        }

        string before = flat[..lambdaIndex].Trim();

        string after = flat[(lambdaIndex + LAMBDA.Length)..].Trim();

        string result;
        if (string.IsNullOrWhiteSpace(after))
        {
            if (string.IsNullOrWhiteSpace(before))
            {
                return UNKNOWN;
            }

            result = before;
        }
        else
        {
            string selector = $"{before}.";
            if (after.StartsWith(selector))
            {
                result = after[selector.Length..];
            }
            else
            {
                result = after;
            }
        }

        if (result.Length > MAX_LENGTH)
        {
            return result[..MAX_LENGTH];
        }

        return result;
    }
}
