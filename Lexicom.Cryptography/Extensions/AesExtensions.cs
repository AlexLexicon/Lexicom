using System.Security.Cryptography;

namespace Lexicom.Cryptography.Extensions;

public static class AesExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static SecretBitSize CalculateSecretSize(this Aes aes, byte[] secretKey)
    {
        ArgumentNullException.ThrowIfNull(aes);
        ArgumentNullException.ThrowIfNull(secretKey);

        var allowedSizes = new HashSet<int>();

        KeySizes[] ks = aes.LegalKeySizes;
        foreach (KeySizes item in ks)
        {
            //a KeySizes describes a range of sizes from MinSize to MaxSize in steps of SkipSize
            //for example AES reports min 128 max 256 skip 64 which includes the 192 bit size
            if (item.SkipSize is 0)
            {
                allowedSizes.Add(item.MinSize);
            }
            else
            {
                for (int size = item.MinSize; size <= item.MaxSize; size += item.SkipSize)
                {
                    allowedSizes.Add(size);
                }
            }
        }

        int bitsSize = secretKey.Length * 8;

        return new SecretBitSize
        {
            Value = bitsSize,
            AllowedSizes = allowedSizes,
        };
    }
}
