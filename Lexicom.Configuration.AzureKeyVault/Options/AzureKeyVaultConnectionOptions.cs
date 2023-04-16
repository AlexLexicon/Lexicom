namespace Lexicom.Configuration.AzureKeyVault.Configurations;
public class AzureKeyVaultConnectionOptions
{
    public string? KeyVaultUrl { get; set; }
    public string? AppRegistrationTenantId { get; set; }
    public string? AppRegistrationClientId { get; set; }
    public string? AppRegistrationClientSecret { get; set; }
}
