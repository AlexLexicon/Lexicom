using Lexicom.Wpf.DependencyInjection;

namespace Lexicom.Mvvm.For.Wpf;
internal class ViewModelStartup<TViewModel> : IStartup where TViewModel : notnull
{
    private readonly IViewModelFactory _dataContextFactory;

    /// <exception cref="ArgumentNullException"/>
    public ViewModelStartup(IViewModelFactory dataContextFactory)
    {
        ArgumentNullException.ThrowIfNull(dataContextFactory);

        _dataContextFactory = dataContextFactory;
    }

    public async Task StartupAsync()
    {
        TViewModel viewModel = _dataContextFactory.Create<TViewModel>();

        if (viewModel is IStartup viewModelStartup)
        {
            await viewModelStartup.StartupAsync();
        }
    }
}
