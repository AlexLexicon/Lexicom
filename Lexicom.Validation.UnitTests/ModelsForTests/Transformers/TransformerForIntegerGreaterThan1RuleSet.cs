﻿namespace Lexicom.Validation.UnitTests.ModelsForTests.Transformers;
public class TransformerForIntegerGreaterThan1RuleSet : AbstractRuleSetTransformer<string?, int>
{
    public override string ErrorMessageTypeName => "Number";

    public override bool TryTransform(string? inProperty, out int property)
    {
        return int.TryParse(inProperty, out property);
    }
}
