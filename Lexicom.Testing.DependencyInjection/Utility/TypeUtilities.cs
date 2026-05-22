using System.Diagnostics;

namespace Lexicom.Testing.DependencyInjection.Utility;

internal static class TypeUtilities
{
    /// <exception cref="ArgumentNullException"/>
    internal static bool IsValueType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.IsValueType)
        {
            return true;
        }

        if (IsNullableType(type))
        {
            Type? genericArgumentType = type
                .GetGenericArguments()
                .FirstOrDefault();

            if (genericArgumentType is null)
            {
                throw new UnreachableException($"The type '{type.Name}' is nullable but does not have a generic argument.");
            }

            return genericArgumentType.IsValueType;
        }

        return false;
    }

    /// <exception cref="ArgumentNullException"/>
    internal static bool IsNullableType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <exception cref="ArgumentNullException"/>
    internal static bool IsAssignableTo(object candidate, Type typeToAssignTo)
    {
        ArgumentNullException.ThrowIfNull(candidate);
        ArgumentNullException.ThrowIfNull(typeToAssignTo);

        if (candidate is null)
        {
            //null is valid for any reference type or Nullable<T>
            return !typeToAssignTo.IsValueType || IsNullableType(typeToAssignTo);
        }

        Type candidateType = candidate.GetType();

        return typeToAssignTo.IsAssignableFrom(candidateType);
    }
}
