namespace Lexicom.Mvvm.Amenities;
public interface IMediatRHandlersProvider<THandler>
{
    IEnumerable<THandler> GetViewModelHandlers();
    IEnumerable<THandler> GetRegularHandlers();
}
public class MediatRHandlersProvider<THandler, TViewModelImplementation> : IMediatRHandlersProvider<THandler> where TViewModelImplementation : class, THandler
{
    private readonly WeakViewModelRefrenceCollection<TViewModelImplementation> _weakViewModelRefrenceCollection;
    private readonly IEnumerable<MediatRHandlerImplementationConflictingWithViewModels<THandler>> _handlerImplementations;

    /// <exception cref="ArgumentNullException"/>
    public MediatRHandlersProvider(
        WeakViewModelRefrenceCollection<TViewModelImplementation> weakViewModelRefrenceCollection,
        IEnumerable<MediatRHandlerImplementationConflictingWithViewModels<THandler>> handlerImplementations)
    {
        ArgumentNullException.ThrowIfNull(weakViewModelRefrenceCollection);
        ArgumentNullException.ThrowIfNull(handlerImplementations);

        _weakViewModelRefrenceCollection = weakViewModelRefrenceCollection;
        _handlerImplementations = handlerImplementations;
    }

    public IEnumerable<THandler> GetViewModelHandlers()
    {
        return _weakViewModelRefrenceCollection.GetRemainingViewModels();
    }

    public IEnumerable<THandler> GetRegularHandlers()
    {
        return _handlerImplementations.Select(hi => hi.Implementation);
    }
}