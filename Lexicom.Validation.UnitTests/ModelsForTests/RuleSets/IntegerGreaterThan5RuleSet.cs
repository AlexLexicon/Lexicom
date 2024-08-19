using FluentValidation;

namespace Lexicom.Validation.UnitTests.ModelsForTests.RuleSets;
public class IntegerGreaterThan5RuleSet : AbstractRuleSet<int>
{
    public override void Use<T>(IRuleBuilderOptions<T, int> ruleBuilder)
    {
        ruleBuilder
            .GreaterThan(5);
    }
}
