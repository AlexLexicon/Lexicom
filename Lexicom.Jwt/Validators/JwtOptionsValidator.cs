using Lexicom.Jwt.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Jwt.Validators;
public class JwtOptionsValidator : AbstractOptionsValidator<JwtOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public JwtOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.SymmetricSecurityKey)
            .UseRuleSet(requiredRuleSet);
    }
}
