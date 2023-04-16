using FluentValidation;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Options;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsValidator : AbstractOptionsValidator<IdentityOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public IdentityOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.ClaimsIdentity)
            .NotNull()
            .SetValidator(new IdentityOptionsClaimsIdentityValidator(requiredRuleSet));

        RuleFor(o => o.User)
            .NotNull()
            .SetValidator(new IdentityOptionsUserValidator(requiredRuleSet));

        RuleFor(o => o.Password)
            .NotNull()
            .SetValidator(new IdentityOptionsPasswordValidator());

        RuleFor(o => o.Lockout)
            .NotNull()
            .SetValidator(new IdentityOptionsLockoutValidator());

        RuleFor(o => o.SignIn)
            .NotNull()
            .SetValidator(new IdentityOptionsSignInValidator());

        RuleFor(o => o.Tokens)
            .NotNull()
            .SetValidator(new IdentityOptionsTokensValidator(requiredRuleSet));

        RuleFor(o => o.Stores)
            .NotNull()
            .SetValidator(new IdentityOptionsStoresValidator());
    }
}
