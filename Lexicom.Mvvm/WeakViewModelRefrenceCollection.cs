namespace Lexicom.Mvvm;
public class WeakViewModelRefrenceCollection<TViewModelImplementation> where TViewModelImplementation : class
{
    private readonly List<WeakReference<TViewModelImplementation>> _weakViewModelRefrences;

    public WeakViewModelRefrenceCollection()
    {
        _weakViewModelRefrences = [];
    }

    /// <exception cref="ArgumentNullException"/>
    public void Add(TViewModelImplementation viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        _weakViewModelRefrences.Add(new WeakReference<TViewModelImplementation>(viewModel));
    }

    public IReadOnlyList<TViewModelImplementation> GetRemainingViewModels()
    {
        var viewModels = new List<TViewModelImplementation>();

        foreach (var weakViewModelRefrence in _weakViewModelRefrences)
        {
            if (weakViewModelRefrence.TryGetTarget(out var viewModel))
            {
                viewModels.Add(viewModel);
            }
        }

        return viewModels;
    }
}
