namespace Lexicom.Mvvm.Amenities;
public interface IMediatRHandlersProvider<THandler>
{
    IEnumerable<THandler> GetHandlers();
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

    public IEnumerable<THandler> GetHandlers()
    {
        var handlers = new List<THandler>();

        var viewModelHandlers = _weakViewModelRefrenceCollection.GetRemainingViewModels();
        var regularHandlers = _handlerImplementations
            .Where(hi => hi is not null && hi.Implementation is not null)
            .Select(hi => hi.Implementation)
            .DistinctBy(i => i!.GetType().FullName);

        handlers.AddRange(viewModelHandlers);
        handlers.AddRange(regularHandlers);

        return handlers;
    }
}