using FluentValidation;

namespace Lexicom.Validation.UnitTests.ModelsForTests.RuleSets;
public class StringDigitsRuleSet : AbstractRuleSet<string?>
{
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .NotNull()
            .Must(s => s is not null && s.All(char.IsDigit))
            .WithMessage("Must contain only digits.");
    }
}
