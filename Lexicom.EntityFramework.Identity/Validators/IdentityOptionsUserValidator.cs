using FluentValidation;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsUserValidator : AbstractValidator<UserOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public IdentityOptionsUserValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.AllowedUserNameCharacters)
            .UseRuleSet(requiredRuleSet);

        //RuleFor(o => o.RequireUniqueEmail);
    }
}
