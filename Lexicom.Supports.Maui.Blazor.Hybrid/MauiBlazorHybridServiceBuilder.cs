namespace Lexicom.Supports.Maui.Blazor.Hybrid;
public interface IMauiBlazorHybridServiceBuilder
{
    IServiceCollection Services { get; }
}
public interface IDependantMauiBlazorHybridServiceBuilder : IMauiBlazorHybridServiceBuilder
{
    MauiAppBuilder MauiAppBuilder { get; }
}
public class MauiBlazorHybridServiceBuilder : IDependantMauiBlazorHybridServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public MauiBlazorHybridServiceBuilder(MauiAppBuilder mauiAppBuilder)
    {
        ArgumentNullException.ThrowIfNull(mauiAppBuilder);

        MauiAppBuilder = mauiAppBuilder;
    }

    public MauiAppBuilder MauiAppBuilder { get; }
    public IServiceCollection Services => MauiAppBuilder.Services;
}