namespace Lexicom.Extensions.Reflection;
public static class ObjectExtensions
{
    public static T? TryCast<T>(this object? instance)
    {
        if (instance is not null && instance.GetType().IsAssignableTo(typeof(T)))
        {
            return (T)instance;
        }

        return default;
    }
}
