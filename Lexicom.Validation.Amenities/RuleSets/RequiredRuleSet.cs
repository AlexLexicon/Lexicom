using FluentValidation;
using Lexicom.Validation.Amenities.Extensions;

namespace Lexicom.Validation.Amenities.RuleSets;
public class RequiredRuleSet : RequiredRuleSet<string?>
{
    /// <exception cref="ArgumentNullException"/>
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces();
    }
}
public class RequiredRuleSet<TProperty> : AbstractRuleSet<TProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public override void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty();
    }
}
