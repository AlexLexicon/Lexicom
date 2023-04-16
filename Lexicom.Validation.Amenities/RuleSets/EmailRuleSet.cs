using FluentValidation;
using Lexicom.Validation.Amenities.Options;
using Lexicom.Validation.Amenities.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Amenities.RuleSets;
public class EmailRuleSet : AbstractRuleSet<string?>
{
    private const int DEFAULT_LENGTH_MINIMUM = 3;
    private const int DEFAULT_LENGTH_MAXIMUM = 254;

    private readonly IOptions<EmailRuleSetOptions> _emailRuleSetOptions;

    /// <exception cref="ArgumentNullException"/>
    public EmailRuleSet(IOptions<EmailRuleSetOptions> emailRuleSetOptions)
    {
        ArgumentNullException.ThrowIfNull(emailRuleSetOptions);

        _emailRuleSetOptions = emailRuleSetOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        EmailRuleSetOptions emailRuleSetConfiguration = _emailRuleSetOptions.Value;

        int? lengthMinimum = emailRuleSetConfiguration.MinimumLength ?? DEFAULT_LENGTH_MINIMUM;
        int? lengthMaximum = emailRuleSetConfiguration.MaximumLength ?? DEFAULT_LENGTH_MAXIMUM;

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace()
            .EmailAddress()
            .Length(lengthMinimum, lengthMaximum);
    }
}
