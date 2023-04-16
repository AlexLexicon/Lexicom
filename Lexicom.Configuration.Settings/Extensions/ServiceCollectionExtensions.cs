using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Configuration.Settings.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomWpfConfigurationSettings(this IServiceCollection services, IApplicationSettingsProvider applicationSettingsProvider)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(applicationSettingsProvider);

        services.AddSingleton(applicationSettingsProvider);

        services.AddSingleton<ISettingsWriter, SettingsWriter>();

        return services;
    }
}
