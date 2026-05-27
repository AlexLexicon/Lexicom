using FluentValidation;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.Extensions;

namespace IntegrationTests.For.Lexicom.Validation.Amenities.Constructs.RuleSets;

public class NotAllDigitsRuleSet : AbstractRuleSet<string?>
{
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .NotAllDigits();
    }
}
