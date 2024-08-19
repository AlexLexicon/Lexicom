using Lexicom.Validation.UnitTests.ModelsForTests.RuleSets;

namespace Lexicom.Validation.UnitTests.ModelsForTests.Transformers;
public class TransformerForIntegerGreaterThan5RuleSetAndStringDigitsRuleSet : AbstractRuleSetTransformer<int, string?, IRuleSetValidator<StringDigitsRuleSet, string?>>
{
    public TransformerForIntegerGreaterThan5RuleSetAndStringDigitsRuleSet(IRuleSetValidator<StringDigitsRuleSet, string?> ruleSetValidator) : base(ruleSetValidator)
    {
    }

    public override string ErrorMessageTypeName => "Number";

    public override bool TryTransform(string? inProperty, out int property)
    {
        return int.TryParse(inProperty, out property);
    }
}
