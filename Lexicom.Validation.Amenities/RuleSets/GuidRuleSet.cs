using FluentValidation;
using Lexicom.Validation.Amenities.Extensions;

namespace Lexicom.Validation.Amenities.RuleSets;
public class GuidRuleSet : GuidRuleSet<Guid>
{
}
public class GuidRuleSet<TProperty> : AbstractRuleSet<TProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public override void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        ruleBuilder
            .Guid()
            .DependentRules(() =>
            {
                ruleBuilder.NotSimplyEmpty();
            });
    }
}