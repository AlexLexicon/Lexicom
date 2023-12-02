namespace Lexicom.Mvvm;
internal class ViewModelImplementationTypeAccessor<TViewModelService> where TViewModelService : notnull
{
    public ViewModelImplementationTypeAccessor(Type viewModelImplementationType)
    {
        ViewModelImplementationType = viewModelImplementationType;
    }

    public Type ViewModelImplementationType { get; }
}
