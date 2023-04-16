using FluentValidation;
using Lexicom.EntityFramework.Identity.Options;
using Lexicom.Validation.Options;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityEmailConfirmationTokenProviderOptionsValidator : AbstractOptionsValidator<EmailConfirmationTokenProviderOptions>
{
    public IdentityEmailConfirmationTokenProviderOptionsValidator()
    {
        RuleFor(o => o.TokenLifespan)
            .GreaterThan(TimeSpan.Zero);
    }
}
