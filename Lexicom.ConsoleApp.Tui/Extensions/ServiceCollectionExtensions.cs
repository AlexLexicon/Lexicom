using Lexicom.ConsoleApp.Tui.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lexicom.ConsoleApp.Tui.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="TuiOperationAssemblyScanException{TAssemblyScanMarker}"/>
    public static IServiceCollection AddLexicomConsoleAppTui<TAssemblyScanMarker>(this IServiceCollection services, ServiceLifetime operationLifetime = ServiceLifetime.Transient)
    {
        ArgumentNullException.ThrowIfNull(services);

        List<Type>? operationTypes = null;

        Exception? assemblyScanException = null;
        try
        {
            operationTypes = Assembly
                .GetAssembly(typeof(TAssemblyScanMarker))?
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(ITuiOperation).IsAssignableFrom(t))
                .ToList();
        }
        catch (Exception e)
        {
            assemblyScanException = e;
        }

        if (assemblyScanException is not null || operationTypes is null)
        {
            throw new TuiOperationAssemblyScanException<TAssemblyScanMarker>(assemblyScanException);
        }

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