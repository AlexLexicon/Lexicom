using FluentValidation;
using Lexicom.Cryptography.Options;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Cryptography.Validators;
public class CryptographyByteSecretOptionsValidator : AbstractOptionsValidator<CryptographyByteSecretOptions>
{
    public CryptographyByteSecretOptionsValidator()
    {
        RuleFor(o => o.ByteArraySecretKey)
            .NotNull()
            .NotSimplyEmpty();
    }
}
