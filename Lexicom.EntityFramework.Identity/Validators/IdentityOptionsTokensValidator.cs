using FluentValidation;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsTokensValidator : AbstractValidator<TokenOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public IdentityOptionsTokensValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.EmailConfirmationTokenProvider)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.PasswordResetTokenProvider)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.ChangeEmailTokenProvider)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.ChangePhoneNumberTokenProvider)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.AuthenticatorTokenProvider)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.AuthenticatorIssuer)
            .UseRuleSet(requiredRuleSet);
    }
}