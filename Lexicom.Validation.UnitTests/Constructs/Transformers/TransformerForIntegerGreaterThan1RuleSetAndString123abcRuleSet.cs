using Lexicom.Validation.UnitTests.Constructs.RuleSets;

namespace Lexicom.Validation.UnitTests.Constructs.Transformers;

public class TransformerForIntegerGreaterThan1RuleSetAndString123abcRuleSet : AbstractRuleSetTransformer<string?, int, IRuleSetValidator<IntegerGreaterThan1RuleSet, int>>
{
    public TransformerForIntegerGreaterThan1RuleSetAndString123abcRuleSet(IRuleSetValidator<IntegerGreaterThan1RuleSet, int> ruleSetValidator) : base(ruleSetValidator)
    {
    }

    public override string ErrorMessageTypeName => "Number";

    public override bool TryTransform(string? inProperty, out int property)
    {
        return int.TryParse(inProperty, out property);
    }
}
