using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicom.Mvvm.For.Wpf;
/// <exception cref="ArgumentNullException"/>
public class WpfViewModelFactory(IServiceProvider serviceProvider) : ViewModelProvider(serviceProvider), IViewModelFactory
{
    public TViewModel Create<TViewModel>() where TViewModel : notnull
    {
        return CreateViewModelAndTryCoupleWindow<TViewModel>();
    }
    /// <exception cref="ArgumentNullException"/>
    public TViewModel Create<TViewModel, TModel>(TModel model) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model);

        TViewModel viewModel = CreateViewModel<TViewModel, TModel>(model);

        TryCoupleWindow(viewModel);

        return viewModel;
    }
    /// <exception cref="ArgumentNullException"/>
    public TViewModel Create<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        TViewModel viewModel = CreateViewModel<TViewModel, TModel1, TModel2>(model1, model2);

        TryCoupleWindow(viewModel);

        return viewModel;
    }
    /// <exception cref="ArgumentNullException"/>
    public TViewModel Create<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        TViewModel viewModel = CreateViewModel<TViewModel, TModel1, TModel2, TModel3>(model1, model2, model3);

        TryCoupleWindow(viewModel);

        return viewModel;
    }

    protected virtual TViewModel CreateViewModelAndTryCoupleWindow<TViewModel>() where TViewModel : notnull => CreateViewModelAndTryCoupleWindow<TViewModel>(out _);
    internal virtual TViewModel CreateViewModelAndTryCoupleWindow<TViewModel>(out Window? window) where TViewModel : notnull
    {
        TViewModel viewModel = CreateViewModel<TViewModel>();

        TryCoupleWindow(viewModel, out window);

        return viewModel;
    }

    /// <exception cref="ArgumentNullException"/>
    protected virtual bool TryCoupleWindow<TViewModel>(TViewModel viewModel) where TViewModel : notnull => TryCoupleWindow(viewModel, out _);
    /// <exception cref="ArgumentNullException"/>
    protected virtual bool TryCoupleWindow<TViewModel>(TViewModel viewModel, out Window? window) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        var viewModelWindowCoupler = _serviceProvider.GetService<IViewModelWindowCoupler<TViewModel>>();

        if (viewModelWindowCoupler is not null)
        {
            window = (Window)ActivatorUtilities.CreateInstance(_serviceProvider, viewModelWindowCoupler.WindowType);

            viewModelWindowCoupler.Couple(viewModel, window);

            return true;
        }

        window = null;

        return false;
    }
}
