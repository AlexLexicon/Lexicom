using FluentValidation;

namespace Lexicom.Validation.UnitTests.ModelsForTests.RuleSets;
public class String123abcRuleSet : AbstractRuleSet<string?>
{
    public const string MESSAGE = "Must contain only the following characters: 1, 2, 3, a, b, c.";

    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .NotNull()
            .Must(s => s is not null && s.All(c => c is '1' or '2' or '3' or 'a' or 'b' or 'c'))
            .WithMessage(MESSAGE);
    }
}
