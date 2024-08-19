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
            .Must(bsk =>
            {
                if (bsk is not null)
                {
                    SecretBitSize size = GetSize(aesProvider, bsk);

                    return size.IsValid;
                }

                return true;
            })
            .WithMessage(o =>
            {
                SecretBitSize size = GetSize(aesProvider, o.Base64StringSecretKey ?? "");

                string message = SecretKeySizeException.GetMessage(size);

                return message.Replace("\'", "");
            });
    }

    private static SecretBitSize GetSize(IAesProvider aesProvider, string secretKey)
    {
        var aes = aesProvider.Create();

        byte[] secretKeyBytes = CryptographyStringSecretProvider.ConvertBase64SecretToBytes(secretKey);

        return aes.CalculateSecretSize(secretKeyBytes);
    }
}
