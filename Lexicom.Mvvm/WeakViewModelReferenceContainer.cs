namespace Lexicom.Mvvm;

public class WeakViewModelReferenceContainer<TViewModelImplementation> where TViewModelImplementation : class
{
    public required int AddedOrder { get; init; }
    public required WeakReference<TViewModelImplementation> WeakReference { get; init; }
}
