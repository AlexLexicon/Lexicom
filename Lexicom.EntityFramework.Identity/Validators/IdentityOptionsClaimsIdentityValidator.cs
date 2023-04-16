using FluentValidation;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsClaimsIdentityValidator : AbstractValidator<ClaimsIdentityOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public IdentityOptionsClaimsIdentityValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.RoleClaimType)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.UserNameClaimType)
           .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.UserIdClaimType)
           .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.EmailClaimType)
           .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.SecurityStampClaimType)
           .UseRuleSet(requiredRuleSet);
    }
}