namespace Lexicom.Mvvm;
internal class ViewModelImplementationTypeAccessor<TViewModelService> where TViewModelService : notnull
{
    public required Type ViewModelImplementationType { get; init; }
}
