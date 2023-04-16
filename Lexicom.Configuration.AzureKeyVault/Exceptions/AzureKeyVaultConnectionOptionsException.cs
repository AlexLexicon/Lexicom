namespace Lexicom.Configuration.AzureKeyVault.Exceptions;
public class AzureKeyVaultConnectionOptionsException : Exception
{
    public AzureKeyVaultConnectionOptionsException(string? message) : base(message)
    {
    }
}
