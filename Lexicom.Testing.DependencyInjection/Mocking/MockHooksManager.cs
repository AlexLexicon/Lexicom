using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Testing.DependencyInjection.Mocking;

public class MockHooksManager
{
    public delegate bool HookDelegate(MockManager manager, Type type);

    private static List<Hook> AvaliableHooks { get; } =
    [
        new Hook("Lexicom.Testing.DependencyInjection.EntityFramework", "Lexicom.Testing.DependencyInjection.EntityFramework.Mocking.MockHook"),
        new Hook("Lexicom.Mvvm.For.Testing", "Lexicom.Mvvm.For.Testing.Mocking.MockHook"),
    ];

    public static bool TryToMockFromHooks(MockManager manager, Type type)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(type);

        bool isMocked = false;
        if (manager.TestAssistant.AssistantConfiguration.IsAutomaticallyUsingMockHooks)
        {
            foreach (Hook hook in AvaliableHooks)
            {
                isMocked = hook.TryToMockFromHook(manager, type);

                if (isMocked)
                {
                    break;
                }
            }
        }

        return isMocked;
    }


    private class Hook
    {
        public Hook(string projectName, string fullyQualifiedTypeName)
        {
            TypeName = $"{fullyQualifiedTypeName}, {projectName}";
        }

        private string TypeName { get; }
        private bool CheckedForHook { get; set; }
        private HookDelegate? ConnectedHookDelegate { get; set; }

        public bool TryToMockFromHook(MockManager manager, Type type)
        {
            if (ConnectedHookDelegate is not null)
            {
                return ConnectedHookDelegate.Invoke(manager, type);
            }

            if (!CheckedForHook)
            {
                CheckedForHook = true;

                Type? hookType = Type.GetType(TypeName);
                if (hookType is not null)
                {
                    const string DELEGATE_PROPERTY_NAME = nameof(HookDelegate);

                    PropertyInfo? delegateProperty = hookType.GetProperty(DELEGATE_PROPERTY_NAME, BindingFlags.Public | BindingFlags.Static);
                    if (delegateProperty is null)
                    {
                        throw new UnreachableException($"The hook type '{hookType.Name}' was found but the delegate property '{DELEGATE_PROPERTY_NAME}' was not which should not be possible.");
                    }

                    object? delegateValue = delegateProperty.GetValue(null);
                    if (delegateValue is not HookDelegate hookDelegate)
                    {
                        throw new UnreachableException($"The hooked delegate '{delegateValue?.GetType()?.Name ?? "null"}' was not of the type '{nameof(HookDelegate)}'.");
                    }

                    ConnectedHookDelegate = hookDelegate;

                    return TryToMockFromHook(manager, type);
                }
            }

            return false;
        }
    }
}
