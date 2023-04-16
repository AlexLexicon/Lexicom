using FluentValidation;
using Lexicom.EntityFramework.Identity.Options;
using Lexicom.Validation.Options;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityPasswordResetTokenProviderOptionsValidator : AbstractOptionsValidator<PasswordResetTokenProviderOptions>
{
    public IdentityPasswordResetTokenProviderOptionsValidator()
    {
        RuleFor(o => o.TokenLifespan)
            .GreaterThan(TimeSpan.Zero);
    }
}
