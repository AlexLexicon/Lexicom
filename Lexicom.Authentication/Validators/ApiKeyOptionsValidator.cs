using FluentValidation;
using Lexicom.Authentication.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Authentication.Validators;

public class ApiKeyOptionsValidator : AbstractOptionsValidator<ApiKeyOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public ApiKeyOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.Keys)
            .Must(ks =>
            {
                return ks is null || ks
                    .Where(d => d.Id is not null)
                    .GroupBy(d => d.Id)
                    .All(g => g.Count() is 1);
            })
            .WithMessage($"'{{PropertyName}}' must have a unique '{nameof(ApiKeyOptionsKey.Id)}' for every api key.")
            .Must(ks =>
            {
                return ks is null || ks
                    .Where(d => d.Key is not null)
                    .GroupBy(d => d.Key)
                    .All(g => g.Count() is 1);
            })
            .WithMessage($"'{{PropertyName}}' must have a unique '{nameof(ApiKeyOptionsKey.Key)}' for every api key.");

        RuleForEach(o => o.Keys)
            .NotNull()
            .ChildRules(k =>
            {
                k.RuleFor(d => d.Id)
                    .NotNull()
                    .NotEqual(Guid.Empty);

                k.RuleFor(d => d.Key)
                    .UseRuleSet(requiredRuleSet);

                k.RuleForEach(d => d.Permissions)
                    .UseRuleSet(requiredRuleSet);

                k.RuleForEach(d => d.Roles)
                    .UseRuleSet(requiredRuleSet);

                k.RuleForEach(d => d.Claims)
                    .Must(c => !string.IsNullOrWhiteSpace(c.Key))
                    .WithMessage("'{PropertyName}' must not have an empty claim type.")
                    .Must(c => !string.IsNullOrWhiteSpace(c.Value))
                    .WithMessage((_, c) => $"'{{PropertyName}}' must have a value for the '{c.Key}' claim.");
            });
    }
}
