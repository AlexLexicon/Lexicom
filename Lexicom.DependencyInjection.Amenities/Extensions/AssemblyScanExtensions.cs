namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class AssemblyScanExtensions
{


    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScan Register(this IAssemblyScan assemblyScan, Action<Type> registerDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(registerDelegate);

        assemblyScan.RegisterDelegates.Add(registerDelegate);

        return assemblyScan;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScan Register(this IAssemblyScan assemblyScan, Func<Type, bool> isRegisteredDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(isRegisteredDelegate);

        assemblyScan.TryRegisterDelegates.Add(isRegisteredDelegate);

        return assemblyScan;
    }
}
