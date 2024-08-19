namespace Lexicom.Cryptography.Exceptions;
public class SecretKeySizeException : Exception
{
    public static string GetMessage(SecretBitSize? secretBitSize)
    {
        string? sizesString = null;
        if (secretBitSize?.AllowedSizes is not null)
        {
            sizesString = string.Join(',', secretBitSize.AllowedSizes.Select(s => s.ToString()));
        }

        return $"The secret key had a length of '{secretBitSize?.Value.ToString() ?? "null"}' bits but has to have a length equal to exactly one of the following values: '{sizesString}'.";
    }

    public SecretKeySizeException(SecretBitSize? secretBitSize) : base(GetMessage(secretBitSize))
    {
    }
}
