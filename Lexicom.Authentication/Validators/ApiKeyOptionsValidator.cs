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

        RuleForEach(o => o.Keys)
            .NotNull()
            .ChildRules(descriptor =>
            {
                descriptor.RuleFor(d => d.Id)
                    .NotNull()
                    .NotEqual(Guid.Empty);

                descriptor.RuleFor(d => d.Key)
                    .UseRuleSet(requiredRuleSet);

                descriptor.RuleForEach(d => d.Permissions)
                    .UseRuleSet(requiredRuleSet);

                descriptor.RuleForEach(d => d.Roles)
                    .UseRuleSet(requiredRuleSet);
            });

        RuleFor(o => o.Keys)
            .Must(keys => keys is null || keys.Where(d => d.Id is not null).GroupBy(d => d.Id).All(g => g.Count() is 1))
            .WithMessage($"Every '{nameof(ApiKeyOptionsDescriptor.Id)}' in '{{PropertyName}}' must be unique.")
            .Must(keys => keys is null || keys.Where(d => d.Key is not null).GroupBy(d => d.Key).All(g => g.Count() is 1))
            .WithMessage($"Every '{nameof(ApiKeyOptionsDescriptor.Key)}' in '{{PropertyName}}' must be unique.");
    }
}
