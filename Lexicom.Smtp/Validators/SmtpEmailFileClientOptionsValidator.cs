using Lexicom.Smtp.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Smtp.Validators;
public class SmtpEmailFileClientOptionsValidator : AbstractOptionsValidator<SmtpEmailFileClientOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public SmtpEmailFileClientOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.OutputDirectoryPath)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.FileName)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.FileExtension)
            .UseRuleSet(requiredRuleSet);
    }
}
