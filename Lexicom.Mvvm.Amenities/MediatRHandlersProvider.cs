namespace Lexicom.Mvvm.Amenities;
public interface IMediatRHandlersProvider<THandler>
{
    IEnumerable<THandler> GetHandlers();
}
public class MediatRHandlersProvider<THandler, TViewModelImplementation> : IMediatRHandlersProvider<THandler> where TViewModelImplementation : class, THandler
{
    private readonly WeakViewModelRefrenceCollection<TViewModelImplementation> _weakViewModelRefrenceCollection;
    private readonly IEnumerable<MediatRHandlerImplementationConflictingWithViewModels<THandler>> _handlerImplementations;

    public MediatRHandlersProvider(
        WeakViewModelRefrenceCollection<TViewModelImplementation> weakViewModelRefrenceCollection,
        IEnumerable<MediatRHandlerImplementationConflictingWithViewModels<THandler>> handlerImplementations)
    {
        _weakViewModelRefrenceCollection = weakViewModelRefrenceCollection;
        _handlerImplementations = handlerImplementations;
    }

    public IEnumerable<THandler> GetHandlers()
    {
        var handlers = new List<THandler>();

        handlers.AddRange(_weakViewModelRefrenceCollection.GetRemainingViewModels());
        handlers.AddRange(_handlerImplementations.Select(hi => hi.Implementation));

        return handlers;
    }
}