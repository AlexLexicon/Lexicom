﻿using Lexicom.DependencyInjection.Amenities.Extensions;

namespace Lexicom.DependencyInjection.Amenities;
public interface IAssemblyScanInital : IAssemblyScan
{
    IAssemblyScanPartial GetPartial();
}
public interface IAssemblyScanPartial : IAssemblyScan
{
    IAssemblyScanFinal GetFinal();
}
public interface IAssemblyScanFinal : IAssemblyScan
{
}
public interface IAssemblyScan
{
    List<Action<Type>> RegisterDelegates { get; }
    List<Func<Type, bool>> TryRegisterDelegates { get; }

    IReadOnlyList<Type> GetTypes();
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

    public abstract IReadOnlyList<Type> GetTypes();
}
public class AssemblyScan<TAssignableTo> : AssemblyScan, IAssemblyScanInital, IAssemblyScanPartial, IAssemblyScanFinal
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

    private bool HasRegistered { get; set; }

    public IAssemblyScanPartial GetPartial() => this;
    public IAssemblyScanFinal GetFinal() => this;

    public override IReadOnlyList<Type> GetTypes()
    {
        Type assignableToType = typeof(TAssignableTo);

        Type[] types;
        if (_assemblyScanOptions.AllowNonExportedTypes)
        {
            types = _assemblyScanMarkerType.Assembly.GetTypes();
        }
        else
        {
            types = _assemblyScanMarkerType.Assembly.GetExportedTypes();
        }

        var registeredTypes = new List<Type>();
        foreach (Type type in types)
        {
            //when 'type' is assignable to 'assignableToType' we execute the 'When' delegate
            //then if the 'type' (t) is registered via 'tryRegisterDelegate' we move to the next type in 'types'
            //however if none of the 'TryRegisterDelegates' result in true
            //we invoke all of the 'RegisterDelegates' for 'type' (t)
            type.When(assignableToType, _assemblyScanOptions.AssignableToOptions, t =>
            {
                //we only want to run registrations once
                //but we still want to allow calling GetTypes
                //as many times as the caller wants
                if (!HasRegistered)
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
                }

                registeredTypes.Add(t);
            });
        }

        HasRegistered = true;

        return registeredTypes;
    }
}
