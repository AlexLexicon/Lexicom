using FluentValidation;
using Lexicom.Smtp.Options;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Smtp.Validators;
public class SmtpEmailMailClientOptionsValidator : AbstractOptionsValidator<SmtpEmailMailClientOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public SmtpEmailMailClientOptionsValidator(
        EmailRuleSet emailRuleSet, 
        RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(emailRuleSet);
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.FromEmailAddress)
            .UseRuleSet(emailRuleSet);

        RuleFor(o => o.Host)
            .UseRuleSet(requiredRuleSet);

        //RuleFor(o => o.Port);

        RuleFor(o => o.IsSslEnabled)
            .NotNull();

        RuleFor(o => o.NetworkCredentialsUsername)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.NetworkCredentialsPassword)
            .NotNull()
            .NotSimplyEmpty();
    }
}
