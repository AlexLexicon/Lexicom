using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm.Support;
public abstract class ViewModelProvider
{
    private static MethodInfo? _staticAddToWeakViewModelRefrenceCollectionMethodInfo;
    private static MethodInfo StaticAddToWeakViewModelRefrenceCollectionMethodInfo => _staticAddToWeakViewModelRefrenceCollectionMethodInfo ??= (typeof(ViewModelProvider).GetMethod(nameof(AddToWeakViewModelRefrenceCollection), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(AddToWeakViewModelRefrenceCollection)}' was not found."));
    private static void AddToWeakViewModelRefrenceCollection<TViewModelImplementation>(IServiceProvider serviceProvider, TViewModelImplementation viewModel) where TViewModelImplementation : class
    {
        var weakViewModelRefrenceCollection = serviceProvider.GetRequiredService<WeakViewModelRefrenceCollection<TViewModelImplementation>>();

        //we need to hold a weak refrence for the way
        //i want my mediatR implenmentation to allow
        //transient view models to still act as handlers
        //but be cleaned up when the view model is no
        //longer needed and not stay around as those handlers
        weakViewModelRefrenceCollection.Add(viewModel);
    }

    protected readonly IServiceProvider _serviceProvider;

    /// <exception cref="ArgumentNullException"/>
    public ViewModelProvider(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _serviceProvider = serviceProvider;
    }

    protected virtual TViewModel CreateViewModel<TViewModel>() where TViewModel : notnull
    {
        return InitializeViewModel(implementationType =>
        {
            return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType);
        });
    }
    /// <exception cref="ArgumentNullException"/>
    protected virtual TViewModel CreateViewModel<TViewModel, TModel>(TModel model) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model);

        return InitializeViewModel(implementationType =>
        {
            return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType, model);
        });
    }
    /// <exception cref="ArgumentNullException"/>
    protected virtual TViewModel CreateViewModel<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        return InitializeViewModel(implementationType =>
        {
            return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType, model1, model2);
        });
    }
    /// <exception cref="ArgumentNullException"/>
    protected virtual TViewModel CreateViewModel<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        return InitializeViewModel(implementationType =>
        {
            return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType, model1, model2, model3);
        });
    }

    protected virtual Type GetViewModelImplementationType<TViewModel>() where TViewModel : notnull
    {
        var viewModelImplementationTypeAccessor = _serviceProvider.GetService<ViewModelImplementationTypeAccessor<TViewModel>>();

        Type ViewModelImplementationType;
        if (viewModelImplementationTypeAccessor is not null)
        {
            ViewModelImplementationType = viewModelImplementationTypeAccessor.ViewModelImplementationType;
        }
        else
        {
            ViewModelImplementationType = typeof(TViewModel);
        }

        return ViewModelImplementationType;
    }

    private TViewModel InitializeViewModel<TViewModel>(Func<Type, TViewModel> activateImplementationTypeDelegate) where TViewModel : notnull
    {
        Type implementationType = GetViewModelImplementationType<TViewModel>();

        TViewModel viewModel = activateImplementationTypeDelegate.Invoke(implementationType);

        StaticAddToWeakViewModelRefrenceCollectionMethodInfo.MakeGenericMethod(implementationType).Invoke(null, new object[] { _serviceProvider, viewModel });

        return viewModel;
    }
}
