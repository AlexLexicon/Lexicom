using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Supports.Maui.Blazor.Hybrid;

public interface IMauiBlazorHybridServiceBuilder
{
    IServiceCollection Services { get; }
}
public interface IDependantMauiBlazorHybridServiceBuilder : IMauiBlazorHybridServiceBuilder
{
    //MauiAppBuilder MauiAppBuilder { get; }
}
public class MauiBlazorHybridServiceBuilder : IDependantMauiBlazorHybridServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    //public MauiBlazorHybridServiceBuilder(MauiAppBuilder mauiAppBuilder)
    //{
    //    ArgumentNullException.ThrowIfNull(mauiAppBuilder);

    //    MauiAppBuilder = mauiAppBuilder;
    //}

    // public MauiAppBuilder MauiAppBuilder { get; }
    public IServiceCollection Services => throw new NotSupportedException("Maui is not yet supported in dot net 10.");//MauiAppBuilder.Services;
}