using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Mocking;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm.For.Testing.Mocking;

public static class MockHook
{
    public static MockHooksManager.HookDelegate HookDelegate { get; } = TryMock;

    private static bool TryMock(MockManager manager, Type type)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(type);

        if (manager.TestAssistant.Category is TestingCategory.IntegrationTest && type.IsAssignableTo(typeof(ObservableObject)))
        {
            StaticMockViewModelMethodInfo
                .MakeGenericMethod(type)
                .Invoke(obj: null, parameters:
                [
                    manager,
                ]);

            return true;
        }

        return false;
    }

    private static MethodInfo StaticMockViewModelMethodInfo => field ??= typeof(MockHook).GetMethod(nameof(MockViewModel), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(MockViewModel)}' was not found.");
    private static void MockViewModel<TViewModel>(MockManager manager) where TViewModel : class
    {
        manager
            .Mock<TViewModel>()
            .With(() =>
            {
                var viewModelFactory = manager.TestAssistant.Make<IViewModelFactory>();

                //ultimately this is never actually going to do what it looks like here because 
                //if the observable object view model is needing to be mocked, meaning it isnt
                //registered already in the service collection, then that also means the factory,
                //here will fail to create it, however that is actually better because we dont
                //want to make a substitute and the error message will tell the user what to do
                return viewModelFactory.Create<TViewModel>();
            });
    }
}
