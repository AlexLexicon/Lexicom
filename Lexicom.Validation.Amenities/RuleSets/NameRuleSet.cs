using FluentValidation;
using Lexicom.Validation.Amenities.Options;
using Lexicom.Validation.Amenities.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Amenities.RuleSets;
public class NameRuleSet : AbstractRuleSet<string?>
{
    private const int DEFAULT_LENGTH_MINIMUM = 1;
    private static readonly int? DEFAULT_LENGTH_MAXIMUM = null;

    private readonly IOptions<NameRuleSetOptions> _nameRuleSetOptions;

    /// <exception cref="ArgumentNullException"/>
    public NameRuleSet(IOptions<NameRuleSetOptions> nameRuleSetOptions)
    {
        ArgumentNullException.ThrowIfNull(nameRuleSetOptions);

        _nameRuleSetOptions = nameRuleSetOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        NameRuleSetOptions nameRuleSetConfiguration = _nameRuleSetOptions.Value;

        int? lengthMinimum = nameRuleSetConfiguration.MinimumLength ?? DEFAULT_LENGTH_MINIMUM;
        int? lengthMaximum = nameRuleSetConfiguration.MaximumLength ?? DEFAULT_LENGTH_MAXIMUM;

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace()
            .NotAnyDigits()
            .Letters()
            .Length(lengthMinimum, lengthMaximum);
    }
}
