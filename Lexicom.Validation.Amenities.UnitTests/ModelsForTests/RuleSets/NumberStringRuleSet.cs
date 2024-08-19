using FluentValidation;
using Lexicom.Validation.Amenities.Extensions;

namespace Lexicom.Validation.Amenities.UnitTests.ModelsForTests.RuleSets;
public class NumberStringRuleSet : AbstractRuleSet<string?>
{
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .Digits()
            .GreaterThanOrEqualTo(3);
    }
}
