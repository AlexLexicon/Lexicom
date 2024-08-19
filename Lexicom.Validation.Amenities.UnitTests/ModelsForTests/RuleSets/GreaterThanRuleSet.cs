using FluentValidation;
using Lexicom.Validation.Amenities.Extensions;

namespace Lexicom.Validation.Amenities.UnitTests.ModelsForTests.RuleSets;
public class GreaterThanRuleSet : AbstractRuleSet<string?>
{
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .GreaterThan(5);
    }
}
