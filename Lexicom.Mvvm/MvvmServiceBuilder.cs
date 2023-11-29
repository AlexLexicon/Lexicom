using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm;
public interface IMvvmServiceBuilder
{
    IServiceCollection Services { get; }
    /// <exception cref="ArgumentNullException"/>
    void AddDeferredRegistration(Action deferredRegistrationAction, int priority);
    void InvokeDeferredRegistrations();
}
public class MvvmServiceBuilder : IMvvmServiceBuilder
{
    private readonly Dictionary<int, List<Action>> _deferredRegistrationActions;

    /// <exception cref="ArgumentNullException"/>
    public MvvmServiceBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _deferredRegistrationActions = [];

        Services = services;
    }

    public IServiceCollection Services { get; }

    /// <exception cref="ArgumentNullException"/>
    public void AddDeferredRegistration(Action deferredRegistrationAction, int priority)
    {
        ArgumentNullException.ThrowIfNull(deferredRegistrationAction);

        if (_deferredRegistrationActions.TryGetValue(priority, out List<Action>? value))
        {
            value.Add(deferredRegistrationAction);
        }
        else
        {
            _deferredRegistrationActions.Add(priority,
            [
                deferredRegistrationAction 
            ]);
        }
    }

    public void InvokeDeferredRegistrations()
    {
        List<List<Action>> deferredRegistrationActionsLists = _deferredRegistrationActions
            .OrderBy(kvp => kvp.Key)
            .Select(kvp => kvp.Value)
            .ToList();

        foreach (List<Action> deferredRegistrationActionsList in deferredRegistrationActionsLists)
        {
            foreach (var  deferredRegistrationAction in deferredRegistrationActionsList)
            {
                deferredRegistrationAction.Invoke();
            }
        }
    }
}
