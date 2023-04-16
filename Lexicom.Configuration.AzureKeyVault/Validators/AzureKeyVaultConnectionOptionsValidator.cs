using Lexicom.Configuration.AzureKeyVault.Configurations;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Configuration.AzureKeyVault.Validators;
public class AzureKeyVaultConnectionOptionsValidator : AbstractOptionsValidator<AzureKeyVaultConnectionOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public AzureKeyVaultConnectionOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.KeyVaultUrl)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.AppRegistrationClientId)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.AppRegistrationClientSecret)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.AppRegistrationTenantId)
            .UseRuleSet(requiredRuleSet);
    }
}
