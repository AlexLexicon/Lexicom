using FluentValidation;
using Lexicom.Validation;

namespace IntegrationTests.For.Lexicom.Validation.Constructs.RuleSets;

public class IntegerGreaterThan1RuleSet : AbstractRuleSet<int>
{
    public override void Use<T>(IRuleBuilderOptions<T, int> ruleBuilder)
    {
        ruleBuilder
            .GreaterThan(1);
    }
}
