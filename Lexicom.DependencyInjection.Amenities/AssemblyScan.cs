using Lexicom.DependencyInjection.Amenities.Exceptions;
using Lexicom.DependencyInjection.Amenities.Extensions;

namespace Lexicom.DependencyInjection.Amenities;
public interface IAssemblyScan
{
    List<Action<Type>> RegisterDelegates { get; }
    List<Func<Type, bool>> TryRegisterDelegates { get; }
}
public interface IAssemblyScan<TAssignableTo> : IAssemblyScan
{
}
public abstract class AssemblyScan : IAssemblyScan
{
    protected AssemblyScan()
    {
        RegisterDelegates = [];
        TryRegisterDelegates = [];
    }

    public List<Action<Type>> RegisterDelegates { get; }
    public List<Func<Type, bool>> TryRegisterDelegates { get; }

    public abstract void Execute();
}
public class AssemblyScan<TAssignableTo> : AssemblyScan, IAssemblyScan<TAssignableTo>
{
    private readonly Type _assemblyScanMarkerType;
    private readonly AssemblyScanOptions _assemblyScanOptions;

    /// <exception cref="ArgumentNullException"/>
    public AssemblyScan(
        Type assemblyScanMarkerType, 
        AssemblyScanOptions assemblyScanOptions)
    {
        ArgumentNullException.ThrowIfNull(assemblyScanMarkerType);
        ArgumentNullException.ThrowIfNull(assemblyScanOptions);

        _assemblyScanMarkerType = assemblyScanMarkerType;
        _assemblyScanOptions = assemblyScanOptions;
    }

    private bool HasExecuted { get; set; }

    /// <exception cref="AssemblyScanAlreadyExecutedException"/>
    public override void Execute()
    {
        Type assignableToType =typeof(TAssignableTo);

        if (HasExecuted)
        {
            throw new AssemblyScanAlreadyExecutedException(_assemblyScanMarkerType, assignableToType);
        }

        HasExecuted = true;

        Type[] types;
        if (_assemblyScanOptions.AllowNonExportedTypes)
        {
            types = _assemblyScanMarkerType.Assembly.GetTypes();
        }
        else
        {
            types = _assemblyScanMarkerType.Assembly.GetExportedTypes();
        }
        
        foreach (Type type in types)
        {
            //when 'type' is assignable to 'assignableToType' we execute the 'When' delegate
            //then if the 'type' (t) is registered via 'tryRegisterDelegate' we move to the next type in 'types'
            //however if none of the 'TryRegisterDelegates' result in true
            //we invoke all of the 'RegisterDelegates' for 'type' (t)
            type.When(assignableToType, _assemblyScanOptions.AssignableToOptions, t =>
            {
                bool isRegistered = false;
                foreach (Func<Type, bool> tryRegisterDelegate in TryRegisterDelegates)
                {
                    isRegistered = tryRegisterDelegate.Invoke(t);

                    if (isRegistered)
                    {
                        //we only register a type once via a try register
                        //so the order you add them matters
                        break;
                    }
                }

                //regular register delegates only run
                //if the type was not registered
                //as part of the try register delegates
                if (!isRegistered)
                {
                    foreach (Action<Type> registerDelegate in RegisterDelegates)
                    {
                        registerDelegate.Invoke(t);
                    }
                }
            });
        }
    }
}
