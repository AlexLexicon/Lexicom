namespace Lexicom.Mvvm.Amenities;
public class MediatRHandlerImplementationConflictingWithViewModels<THandler>
{
    public MediatRHandlerImplementationConflictingWithViewModels(THandler implementation)
    {
        Implementation = implementation;
    }

    public THandler Implementation { get; }
}
