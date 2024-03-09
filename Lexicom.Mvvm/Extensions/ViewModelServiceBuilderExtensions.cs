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

        builder.Forward(typeof(TForwardingType));

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ImplementationTypeDoesNotImplementForwardingTypeException"/>
    public static IViewModelServiceBuilder Forward(this IViewModelServiceBuilder builder, Type forwardingType)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(forwardingType);

        if (!builder.ImplementationType.IsAssignableTo(forwardingType))
        {
            throw new ImplementationTypeDoesNotImplementForwardingTypeException(builder.ImplementationType, forwardingType);
        }

        builder.Services.Add(new ServiceDescriptor(forwardingType, sp =>
        {
            return sp.GetRequiredService(builder.ImplementationType);
        }, builder.ServiceLifetime));

        return builder;
    }
}
