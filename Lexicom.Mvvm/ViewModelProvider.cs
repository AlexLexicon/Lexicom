namespace Lexicom.Mvvm;

public interface IViewModelProvider<TViewModel> where TViewModel : class
{
    IReadOnlyList<TViewModel> GetViewModels();
}
public interface IViewModelProvider<TViewModelService, TViewModelImplementation> : IViewModelProvider<TViewModelService> where TViewModelService : class where TViewModelImplementation : class, TViewModelService
{
}
public class ViewModelProvider<TViewModel> : ViewModelProvider<TViewModel, TViewModel> where TViewModel : class
{
    /// <exception cref="ArgumentNullException"></exception>
    public ViewModelProvider(IWeakViewModelReferenceCollection<TViewModel> weakViewModelReferenceCollection) : base(weakViewModelReferenceCollection)
    {
    }
}
public class ViewModelProvider<TViewModelService, TViewModelImplementation> : IViewModelProvider<TViewModelService, TViewModelImplementation> where TViewModelService : class where TViewModelImplementation : class, TViewModelService
{
    private readonly IWeakViewModelReferenceCollection<TViewModelImplementation> _weakViewModelReferenceCollection;

    /// <exception cref="ArgumentNullException"></exception>
    public ViewModelProvider(IWeakViewModelReferenceCollection<TViewModelImplementation> weakViewModelReferenceCollection)
    {
        ArgumentNullException.ThrowIfNull(weakViewModelReferenceCollection);

        _weakViewModelReferenceCollection = weakViewModelReferenceCollection;
    }

    public IReadOnlyList<TViewModelService> GetViewModels()
    {
        var viewModels = new List<TViewModelService>();
        foreach (TViewModelImplementation? viewModel in _weakViewModelReferenceCollection)
        {
            //if the view model is disposed we should not include it
            if (viewModel is not null && (viewModel is not DisposableObservableObject disposableViewModel || !disposableViewModel.IsDisposed))
            {
                viewModels.Add(viewModel);
            }
        }

        return viewModels;
    }
}
