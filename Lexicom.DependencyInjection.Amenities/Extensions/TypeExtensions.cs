namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class TypeExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static bool When<TAssignableTo>(this Type type, Action<Type> whenAssignableDelegate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(whenAssignableDelegate);

        return When<TAssignableTo>(type, options: null, whenAssignableDelegate);
    }
    /// <exception cref="ArgumentNullException"/>
    public static bool When<TAssignableTo>(this Type type, AssignableToOptions? options, Action<Type> whenAssignableDelegate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(whenAssignableDelegate);

        return When(type, typeof(TAssignableTo), options, whenAssignableDelegate);
    }
    /// <exception cref="ArgumentNullException"/>
    public static bool When(this Type type, Type assignableToType, Action<Type> whenAssignableDelegate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(assignableToType);
        ArgumentNullException.ThrowIfNull(whenAssignableDelegate);

        return When(type, assignableToType, options: null, whenAssignableDelegate);
    }
    /// <exception cref="ArgumentNullException"/>
    public static bool When(this Type type, Type assignableToType, AssignableToOptions? options, Action<Type> whenAssignableDelegate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(assignableToType);
        ArgumentNullException.ThrowIfNull(whenAssignableDelegate);

        options ??= AssignableToOptions.Default;

        if (type.IsAssignableTo(assignableToType) && (!type.IsAbstract || options.AllowAbstract) && (!type.IsInterface || options.AllowInterfaces))
        {
            whenAssignableDelegate.Invoke(type);

            return true;
        }

        return false;
    }
}
