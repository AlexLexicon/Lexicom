namespace Lexicom.Mvvm;

public class WeakViewModelRefrenenceContainer<TViewModelImplementation> where TViewModelImplementation : class
{
    public required int AddedOrder { get; init; }
    public required WeakReference<TViewModelImplementation> WeakReference { get; init; }
}
