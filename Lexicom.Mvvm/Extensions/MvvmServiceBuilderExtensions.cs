namespace Lexicom.Mvvm.Extensions;
public static class MvvmServiceBuilderExtensions
{
    public static IMvvmServiceBuilder AddViewModels(this IMvvmServiceBuilder builder, Action<IMvvmViewModelsServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new MvvmViewModelsServiceBuilder(builder));

        return builder;
    }
}
