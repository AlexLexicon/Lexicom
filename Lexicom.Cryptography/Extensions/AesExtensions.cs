using System.Security.Cryptography;

namespace Lexicom.Cryptography.Extensions;
public static class AesExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static SecretBitSize CalculateSecretSize(this Aes aes, byte[] secretKey)
    {
        ArgumentNullException.ThrowIfNull(aes);

        var allowedSizes = new HashSet<int>();

        KeySizes[] ks = aes.LegalKeySizes;
        foreach (KeySizes item in ks)
        {
            allowedSizes.Add(item.MinSize);
            allowedSizes.Add(item.MaxSize);
        }

        int bitsSize = secretKey.Length * 8;

        return new SecretBitSize
        {
            Value = bitsSize,
            AllowedSizes = allowedSizes,
        };
    }
}
