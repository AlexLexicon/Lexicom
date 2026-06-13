using FluentValidation;
using Lexicom.Cryptography.Exceptions;
using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Cryptography.Validators;

public class CryptographyStringSecretOptionsValidator : AbstractOptionsValidator<CryptographyStringSecretOptions>
{
    public CryptographyStringSecretOptionsValidator(IAesProvider aesProvider)
    {
        RuleFor(o => o.Base64StringSecretKey)
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace()
            .Must(b64ssk =>
            {
                if (b64ssk is not null)
                {
                    SecretBitSize? size = GetSizeOrNull(aesProvider, b64ssk);

                    return size is not null && size.IsValid;
                }

                return true;
            })
            .WithMessage(o =>
            {
                SecretBitSize? size = GetSizeOrNull(aesProvider, o.Base64StringSecretKey ?? string.Empty);

                if (size is null)
                {
                    return "'{PropertyName}' must be a valid base64 string.";
                }

                string message = SecretKeySizeException.GetMessage(size);

                return message.Replace("\'", string.Empty);
            });
    }

    private static SecretBitSize? GetSizeOrNull(IAesProvider aesProvider, string secretKey)
    {
        byte[] secretKeyBytes;
        try
        {
            secretKeyBytes = CryptographyStringSecretProvider.ConvertBase64SecretToBytes(secretKey);
        }
        catch (FormatException)
        {
            //the secret key was not a valid base64 string so a size cannot be calculated
            return null;
        }

        using var aes = aesProvider.Create();

        return aes.CalculateSecretSize(secretKeyBytes);
    }
}
