using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;

namespace Lexicom.Mvvm.For.Wpf;

/// <exception cref="ArgumentNullException"/>
public class WpfViewModelFactory(IServiceProvider serviceProvider, IEnumerable<IMessenger> messengers) : ViewModelFactory(serviceProvider, messengers)
{
    public override TViewModel Create<TViewModel>()
    {
        return CreateViewModelAndTryCoupleWindow<TViewModel>(out _);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SingletonViewModelAlreadyExistsException"/>
    public override TViewModel Create<TViewModel, TModel>(TModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        TViewModel viewModel = base.Create<TViewModel, TModel>(model);

        TryCoupleWindow(viewModel, out _);

        return viewModel;
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SingletonViewModelAlreadyExistsException"/>
    public override TViewModel Create<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2)
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        TViewModel viewModel = base.Create<TViewModel, TModel1, TModel2>(model1, model2);

        TryCoupleWindow(viewModel, out _);

        return viewModel;
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SingletonViewModelAlreadyExistsException"/>
    public override TViewModel Create<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3)
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        TViewModel viewModel = base.Create<TViewModel, TModel1, TModel2, TModel3>(model1, model2, model3);

        TryCoupleWindow(viewModel, out _);

        return viewModel;
    }

    public virtual TViewModel CreateViewModelAndTryCoupleWindow<TViewModel>(out Window? window) where TViewModel : notnull
    {
        TViewModel viewModel = base.Create<TViewModel>();

        TryCoupleWindow(viewModel, out window);

        return viewModel;
    }

    /// <exception cref="ArgumentNullException"/>
    protected virtual bool TryCoupleWindow<TViewModel>(TViewModel viewModel, out Window? window) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        var viewModelWindowCoupler = _serviceProvider.GetService<IViewModelWindowCoupler<TViewModel>>();

        if (viewModelWindowCoupler is not null)
        {
            object createdPossibleWindow = ActivatorUtilities.CreateInstance(_serviceProvider, viewModelWindowCoupler.WindowType);

            if (createdPossibleWindow is not Window createdWindow)
            {
                throw new UnreachableException($"The window type '{viewModelWindowCoupler.WindowType?.Name ?? "null"}' is not of the type '{nameof(Window)}' but that shouldn't be possible.");
            }

            window = createdWindow;

            viewModelWindowCoupler.Couple(viewModel, window);

            return true;
        }

        window = null;

        return false;
    }
}
