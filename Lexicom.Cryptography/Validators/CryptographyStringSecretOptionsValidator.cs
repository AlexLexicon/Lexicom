using FluentValidation;
using Lexicom.Cryptography.Options;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Cryptography.Validators;
public class CryptographyStringSecretOptionsValidator : AbstractOptionsValidator<CryptographyStringSecretOptions>
{
    public CryptographyStringSecretOptionsValidator()
    {
        RuleFor(o => o.Base64StringSecretKey)
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace();
    }
}
