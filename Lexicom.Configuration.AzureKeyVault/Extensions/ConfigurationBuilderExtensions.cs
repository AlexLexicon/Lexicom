using Azure.Identity;
using FluentValidation.Results;
using Lexicom.Configuration.AzureKeyVault.Configurations;
using Lexicom.Configuration.AzureKeyVault.Exceptions;
using Lexicom.Configuration.AzureKeyVault.Validators;
using Lexicom.Validation.Amenities.RuleSets;
using Microsoft.Extensions.Configuration;

namespace Lexicom.Configuration.AzureKeyVault.Extensions;
public static class ConfigurationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="AzureKeyVaultConnectionOptionsException"/>
    public static IConfigurationBuilder AddLexicomAzureKeyVault(this IConfigurationBuilder configurationBuilder)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);

        var azureKeyVaultConnectionOptions = new AzureKeyVaultConnectionOptions();

        configurationBuilder
            .Build()
            .Bind(nameof(AzureKeyVaultConnectionOptions), azureKeyVaultConnectionOptions);

        var validator = new AzureKeyVaultConnectionOptionsValidator(new RequiredRuleSet());

        ValidationResult validationResult = validator.Validate(azureKeyVaultConnectionOptions);

        if (!validationResult.IsValid || string.IsNullOrWhiteSpace(azureKeyVaultConnectionOptions.KeyVaultUrl))
        {
            throw new AzureKeyVaultConnectionOptionsException(validationResult.ToString(", "));
        }

        var keyVaultUri = new Uri(azureKeyVaultConnectionOptions.KeyVaultUrl);

        var clientSecretCredential = new ClientSecretCredential(azureKeyVaultConnectionOptions.AppRegistrationTenantId, azureKeyVaultConnectionOptions.AppRegistrationClientId, azureKeyVaultConnectionOptions.AppRegistrationClientSecret);

        configurationBuilder.AddAzureKeyVault(keyVaultUri, clientSecretCredential);

        return configurationBuilder;
    }
}
