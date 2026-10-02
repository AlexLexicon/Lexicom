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
            .WithMessage($"Every '{nameof(ApiKeyOptionsKey.Id)}' in '{{PropertyName}}' must be unique.")
            .Must(ks =>
            {
                return ks is null || ks
                    .Where(d => d.Key is not null)
                    .GroupBy(d => d.Key)
                    .All(g => g.Count() is 1);
            })
            .WithMessage($"Every '{nameof(ApiKeyOptionsKey.Key)}' in '{{PropertyName}}' must be unique.");

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
            });
    }
}
