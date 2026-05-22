using Lexicom.DependencyInjection.Primitives.For.Testing.Exceptions;

namespace Lexicom.DependencyInjection.Primitives.For.Testing.Extensions;

public static class TimerExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{ITimer, TestTimer}"/>
    public static void SetChange(this ITimer timer, bool change)
    {
        ArgumentNullException.ThrowIfNull(timer);

        TestTimer testTimer = GetTestTimer(timer);

        testTimer.SetChange(change);
    }

    private static TestTimer GetTestTimer(ITimer timer)
    {
        if (timer is not TestTimer testTimer)
        {
            throw new NonTestProviderExtensionException<ITimer, TestTimer>();
        }

        return testTimer;
    }
}
