using Lexicom.Configuration.AzureKeyVault.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Configuration.AzureKeyVault.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddAzureKeyVault(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Configuration.AddLexicomAzureKeyVault();

        return builder;
    }
}
