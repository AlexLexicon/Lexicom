using Lexicom.Extensions.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.ConsoleApp.Tui.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="AssemblyScanException{TAssemblyScanMarker, TInterface}"/>
    public static IServiceCollection AddLexicomConsoleAppTui<TAssemblyScanMarker>(this IServiceCollection services, ServiceLifetime operationLifetime = ServiceLifetime.Transient)
    {
        ArgumentNullException.ThrowIfNull(services);

        List<Type> operationTypes = AssemblyScanner.GetConcreteTypesImplementingInterface<TAssemblyScanMarker, ITuiOperation>();

        services.AddSingleton<ITuiConsoleApp, TuiConsoleApp>();

        var operationProvider = new TuiOperationsProvider(operationTypes);

        services.AddSingleton<IAtlasOperationsProvider>(operationProvider);

        foreach (Type operationType in operationProvider.OperationTypes)
        {
            services.Add(new ServiceDescriptor(operationType, operationType, operationLifetime));
        }

        return services;
    }
}