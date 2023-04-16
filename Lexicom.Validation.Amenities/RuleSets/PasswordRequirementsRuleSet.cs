using FluentValidation;
using Lexicom.Validation.Amenities.Options;
using Lexicom.Validation.Amenities.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Amenities.RuleSets;
public class PasswordRequirementsRuleSet : AbstractRuleSet<string?>
{
    //standard microsoft identity password requirements
    private const int DEFAULT_LENGTH_MINIMUM = 6;
    private static readonly int? DEFAULT_LENGTH_MAXIMUM = null;
    private const bool DEFAULT_REQUIRES_DIGIT = true;
    private const bool DEFAULT_REQUIRES_UPPER = true;
    private const bool DEFAULT_REQUIRES_LOWER = true;
    private const bool DEFAULT_REQUIRES_NONALPHANUMERIC = true;
    private const int REQUIRED_UNIQUE_CHARS = 1;

    private readonly IOptions<PasswordRequirementsRuleSetOptions> _passwordRequirementsRuleSetOptions;

    /// <exception cref="ArgumentNullException"/>
    public PasswordRequirementsRuleSet(IOptions<PasswordRequirementsRuleSetOptions> passwordRequirementsRuleSetOptions)
    {
        ArgumentNullException.ThrowIfNull(passwordRequirementsRuleSetOptions);

        _passwordRequirementsRuleSetOptions = passwordRequirementsRuleSetOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        PasswordRequirementsRuleSetOptions passwordRequirementsRuleSetConfiguration = _passwordRequirementsRuleSetOptions.Value;

        int? lengthMinimum = passwordRequirementsRuleSetConfiguration.MinimumLength ?? DEFAULT_LENGTH_MINIMUM;
        int? lengthMaximum = passwordRequirementsRuleSetConfiguration.MaximumLength ?? DEFAULT_LENGTH_MAXIMUM;

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .Length(lengthMinimum, lengthMaximum);

        bool? requiresDigit = passwordRequirementsRuleSetConfiguration.RequireDigit ?? DEFAULT_REQUIRES_DIGIT;
        bool? requiresUpper = passwordRequirementsRuleSetConfiguration.RequireUppercase ?? DEFAULT_REQUIRES_UPPER;
        bool? requiresLower = passwordRequirementsRuleSetConfiguration.RequireLowercase ?? DEFAULT_REQUIRES_LOWER;
        bool? requireNonAlphanumeric = passwordRequirementsRuleSetConfiguration.RequireNonAlphanumeric ?? DEFAULT_REQUIRES_NONALPHANUMERIC;
        int? requiredUniqueChars = passwordRequirementsRuleSetConfiguration.RequiredUniqueChars ?? REQUIRED_UNIQUE_CHARS;

        if (requiresDigit == true)
        {
            ruleBuilder.AnyDigits();
        }

        if (requiresUpper == true)
        {
            ruleBuilder.AnyUpperCaseCharacters();
        }

        if (requiresLower == true)
        {
            ruleBuilder.AnyLowerCaseCharacters();
        }

        if (requireNonAlphanumeric == true)
        {
            ruleBuilder.AnyNonAlphanumeric();
        }

        if (requiredUniqueChars is not null && requiredUniqueChars.Value != 1)
        {
            ruleBuilder
                .Must(p => p is null || p is not null && p.Distinct().Count() >= requiredUniqueChars.Value)
                .WithMessage($"'{{PropertyName}}' must use at least {requiredUniqueChars.Value} unique characters.");
        }
    }
}