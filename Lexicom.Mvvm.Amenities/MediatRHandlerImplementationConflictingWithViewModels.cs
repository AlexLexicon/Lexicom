namespace Lexicom.Mvvm.Amenities;
public class MediatRHandlerImplementationConflictingWithViewModels<THandler>(THandler implementation)
{
    public THandler Implementation { get; } = implementation;
}
