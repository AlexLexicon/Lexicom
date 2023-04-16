using FluentValidation;
using Lexicom.EntityFramework.Identity.Options;
using Lexicom.Validation.Options;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityChangeEmailTokenProviderOptionsValidator : AbstractOptionsValidator<ChangeEmailTokenProviderOptions>
{
    public IdentityChangeEmailTokenProviderOptionsValidator()
    {
        RuleFor(o => o.TokenLifespan)
            .GreaterThan(TimeSpan.Zero);
    }
}
