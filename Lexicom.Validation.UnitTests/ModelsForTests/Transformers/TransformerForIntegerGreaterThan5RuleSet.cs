namespace Lexicom.Validation.UnitTests.ModelsForTests.Transformers;
public class TransformerForIntegerGreaterThan5RuleSet : AbstractRuleSetTransformer<int, string?>
{
    public override string ErrorMessageTypeName => "Number";

    public override bool TryTransform(string? inProperty, out int property)
    {
        return int.TryParse(inProperty, out property);
    }
}
