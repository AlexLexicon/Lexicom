using FluentValidation;

namespace Lexicom.Validation.UnitTests.Constructs.RuleSets;

public class IntegerGreaterThan1RuleSet : AbstractRuleSet<int>
{
    public override void Use<T>(IRuleBuilderOptions<T, int> ruleBuilder)
    {
        ruleBuilder
            .GreaterThan(1);
    }
}
