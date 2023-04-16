using Lexicom.Mvvm.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Extensions;
public static class ViewModelServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ImplementationTypeDoesNotImplementForwardingTypeException"/>
    public static IViewModelServiceBuilder Forward<TForwardingType>(this IViewModelServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        Type forwardType = typeof(TForwardingType);

        if (!builder.ImplementationType.IsAssignableTo(forwardType))
        {
            throw new ImplementationTypeDoesNotImplementForwardingTypeException(builder.ImplementationType, forwardType);
        }

        builder.Services.Add(new ServiceDescriptor(forwardType, sp =>
        {
            return sp.GetRequiredService(builder.ImplementationType);
        }, builder.ServiceLifetime));

        return builder;
    }
}
