namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class AssemblyScanExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanFinal Register(this IAssemblyScanInital assemblyScan, Action<Type> registerDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(registerDelegate);

        SharedRegister(assemblyScan, registerDelegate);

        return assemblyScan
            .GetPartial()
            .GetFinal();
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanPartial TryToRegister(this IAssemblyScanInital assemblyScan, Func<Type, bool> isRegisteredDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(isRegisteredDelegate);

        SharedTryRegister(assemblyScan, isRegisteredDelegate);

        return assemblyScan.GetPartial();
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanPartial TryToRegister<TWhen>(this IAssemblyScanInital assemblyScan, Action<Type> registerDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(registerDelegate);

        SharedTryRegister<TWhen>(assemblyScan, registerDelegate);

        return assemblyScan.GetPartial();
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanPartial ThenTryToRegister(this IAssemblyScanPartial assemblyScan, Func<Type, bool> isRegisteredDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(isRegisteredDelegate);

        SharedTryRegister(assemblyScan, isRegisteredDelegate);

        return assemblyScan;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanPartial ThenTryToRegister<TWhen>(this IAssemblyScanPartial assemblyScan, Action<Type> registerDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(registerDelegate);

        SharedTryRegister<TWhen>(assemblyScan, registerDelegate);

        return assemblyScan;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanFinal OtherwiseRegister(this IAssemblyScanPartial assemblyScan, Action<Type> registerDelegate)
    {
        ArgumentNullException.ThrowIfNull(assemblyScan);
        ArgumentNullException.ThrowIfNull(registerDelegate);

        SharedRegister(assemblyScan, registerDelegate);

        return assemblyScan.GetFinal();
    }

    private static void SharedRegister(IAssemblyScan assemblyScan, Action<Type> registerDelegate)
    {
        assemblyScan.RegisterDelegates.Add(registerDelegate);
    }
    private static void SharedTryRegister<TWhen>(IAssemblyScan assemblyScan, Action<Type> registerDelegate)
    {
        SharedTryRegister(assemblyScan, t => t.When<TWhen>(registerDelegate));
    }
    private static void SharedTryRegister(IAssemblyScan assemblyScan, Func<Type, bool> isRegisteredDelegate)
    {
        assemblyScan.TryRegisterDelegates.Add(isRegisteredDelegate);
    }
}
