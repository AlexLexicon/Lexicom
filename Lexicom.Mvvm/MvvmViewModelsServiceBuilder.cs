namespace Lexicom.Mvvm;
public interface IMvvmViewModelsServiceBuilder
{
    IMvvmServiceBuilder Builder { get; }
}
public class MvvmViewModelsServiceBuilder : IMvvmViewModelsServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public MvvmViewModelsServiceBuilder(IMvvmServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        Builder = builder;
    }

    public IMvvmServiceBuilder Builder { get; }
}
