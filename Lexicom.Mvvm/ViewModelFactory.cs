using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm.Exceptions;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm;

public interface IViewModelFactory
{
    TViewModel Create<TViewModel>() where TViewModel : notnull;
    TViewModel Create<TViewModel, TModel>(TModel model) where TViewModel : notnull;
    TViewModel Create<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TViewModel : notnull;
    TViewModel Create<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TViewModel : notnull;
}
/// <exception cref="ArgumentNullException"/>
public class ViewModelFactory : IViewModelFactory
{
    private static MethodInfo StaticGetWeakViewModelReferenceCollectionMethodInfo => field ??= (typeof(ViewModelFactory).GetMethod(nameof(GetViewModelReferenceCollection), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(GetViewModelReferenceCollection)}' was not found."));
    private static IWeakViewModelReferenceCollection GetViewModelReferenceCollection<TViewModelImplementation>(IServiceProvider serviceProvider) where TViewModelImplementation : class
    {
        try
        {
            return serviceProvider.GetRequiredService<IWeakViewModelReferenceCollection<TViewModelImplementation>>();
        }
        catch (InvalidOperationException e)
        {
            throw new ViewModelNotRegisteredException(typeof(TViewModelImplementation), e);
        }
    }

    protected readonly IServiceProvider _serviceProvider;
    protected readonly IEnumerable<IMessenger> _messengers;

    /// <exception cref="ArgumentNullException"/>
    public ViewModelFactory(IServiceProvider serviceProvider, IEnumerable<IMessenger> messengers)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(messengers);

        _serviceProvider = serviceProvider;
        _messengers = messengers;

        ViewModelTypeToSingletonInstance = [];
        InitializeViewModelLock = new Lock();
    }

    private Dictionary<Type, object> ViewModelTypeToSingletonInstance { get; }
    private Lock InitializeViewModelLock { get; }

    public virtual TViewModel Create<TViewModel>() where TViewModel : notnull
    {
        return InitializeViewModel(isWithModels: false, CreateInstance<TViewModel>);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SingletonViewModelAlreadyExistsException"/>
    public virtual TViewModel Create<TViewModel, TModel>(TModel model) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model);

        return InitializeViewModel(isWithModels: true, implementationType =>
        {
            return CreateInstance<TViewModel, TModel>(implementationType, model);
        });
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SingletonViewModelAlreadyExistsException"/>
    public virtual TViewModel Create<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        return InitializeViewModel(isWithModels: true, implementationType =>
        {
            return CreateInstance<TViewModel, TModel1, TModel2>(implementationType, model1, model2);
        });
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SingletonViewModelAlreadyExistsException"/>
    public virtual TViewModel Create<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        return InitializeViewModel(isWithModels: true, implementationType =>
        {
            return CreateInstance<TViewModel, TModel1, TModel2, TModel3>(implementationType, model1, model2, model3);
        });
    }

    protected virtual TViewModel CreateInstance<TViewModel>(Type implementationType)
    {
        return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType);
    }
    protected virtual TViewModel CreateInstance<TViewModel, TModel>(Type implementationType, TModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType, model);
    }
    protected virtual TViewModel CreateInstance<TViewModel, TModel1, TModel2>(Type implementationType, TModel1 model1, TModel2 model2)
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType, model1, model2);
    }
    protected virtual TViewModel CreateInstance<TViewModel, TModel1, TModel2, TModel3>(Type implementationType, TModel1 model1, TModel2 model2, TModel3 model3)
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        return (TViewModel)ActivatorUtilities.CreateInstance(_serviceProvider, implementationType, model1, model2, model3);
    }

    protected virtual Type GetViewModelImplementationType<TViewModel>() where TViewModel : notnull
    {
        var viewModelImplementationTypeAccessors = _serviceProvider.GetService<IEnumerable<ViewModelImplementationTypeAccessor<TViewModel>>>();
        ViewModelImplementationTypeAccessor<TViewModel>? viewModelImplementationTypeAccessor = viewModelImplementationTypeAccessors?.FirstOrDefault();

        Type viewModelImplementationType;
        if (viewModelImplementationTypeAccessor is not null)
        {
            viewModelImplementationType = viewModelImplementationTypeAccessor.ViewModelImplementationType;
        }
        else
        {
            viewModelImplementationType = typeof(TViewModel);
        }

        return viewModelImplementationType;
    }

    protected virtual ServiceLifetime GetViewModelServiceLifetime<TViewModel>() where TViewModel : notnull
    {
        var viewModelRegistrations = _serviceProvider.GetService<IEnumerable<ViewModelRegistration>>();
        ViewModelRegistration? viewModelRegistration = viewModelRegistrations?.FirstOrDefault(vmr => vmr.ServiceType == typeof(TViewModel));

        ServiceLifetime serviceLifetime;
        if (viewModelRegistration is not null)
        {
            serviceLifetime = viewModelRegistration.ServiceLifetime;
        }
        else
        {
            serviceLifetime = ServiceLifetime.Transient;
        }

        return serviceLifetime;
    }

    protected virtual TViewModel InitializeViewModel<TViewModel>(bool isWithModels, Func<Type, TViewModel> activateImplementationTypeDelegate) where TViewModel : notnull
    {
        //protect multi thread acess for singletons
        lock (InitializeViewModelLock)
        {
            Type viewModelType = typeof(TViewModel);
            Type implementationType = GetViewModelImplementationType<TViewModel>();
            ServiceLifetime serviceLifetime = GetViewModelServiceLifetime<TViewModel>();

            TViewModel? viewModel = default;
            if (serviceLifetime is ServiceLifetime.Singleton && ViewModelTypeToSingletonInstance.TryGetValue(viewModelType, out object? instance))
            {
                viewModel = (TViewModel)instance;

                if (isWithModels)
                {
                    throw new SingletonViewModelAlreadyExistsException(viewModelType);
                }
            }

            if (viewModel is null)
            {
                IWeakViewModelReferenceCollection? weakViewModelReferenceCollection;
                try
                {
                    weakViewModelReferenceCollection = (IWeakViewModelReferenceCollection?)StaticGetWeakViewModelReferenceCollectionMethodInfo
                        .MakeGenericMethod(implementationType)
                        .Invoke(null, [_serviceProvider]);
                }
                catch (TargetInvocationException e) when (e.InnerException is ViewModelNotRegisteredException viewModelNotRegisteredException)
                {
                    //unwrap the TargetInvocationException into the actual ViewModelNotRegisteredException.
                    throw viewModelNotRegisteredException;
                }

                if (weakViewModelReferenceCollection is null)
                {
                    throw new ViewModelNotRegisteredException(implementationType, innerException: null);
                }

                try
                {
                    viewModel = activateImplementationTypeDelegate.Invoke(implementationType);
                }
                catch (InvalidOperationException e)
                {
                    if (!string.IsNullOrWhiteSpace(e.Message) && e.Message.StartsWith("Unable to resolve service for type "))
                    {
                        int start = e.Message.IndexOf('\'') + 1;
                        int end = e.Message.IndexOf('\'', start);
                        string unresolvedTypeName = e.Message[start..end];

                        Type? unresolvedType = Type.GetType(unresolvedTypeName);

                        if (unresolvedType is null)
                        {
                            unresolvedType = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .Select(a => a.GetType(unresolvedTypeName))
                                .FirstOrDefault(t => t is not null);
                        }

                        if (unresolvedType is not null && typeof(ObservableObject).IsAssignableFrom(unresolvedType))
                        {
                            throw new ViewModelNotRegisteredException(unresolvedType, e);
                        }
                    }

                    throw;
                }

                weakViewModelReferenceCollection.Add(viewModel);

                if (serviceLifetime is ServiceLifetime.Singleton)
                {
                    if (ViewModelTypeToSingletonInstance.ContainsKey(viewModelType))
                    {
                        //if the type is a singleton then we should have been able to access the instance above thus this should not be possible.
                        throw new UnreachableException($"The view model '{viewModelType?.FullName ?? "null"}' has already been created.");
                    }

                    ViewModelTypeToSingletonInstance.Add(viewModelType, viewModel);
                }

                foreach (IMessenger? messenger in _messengers)
                {
                    if (messenger is not null)
                    {
                        messenger?.RegisterAll(viewModel);
                        messenger?.AsyncRegisterAll(viewModel);
                    }
                }
            }

            return viewModel;
        }
    }
}
