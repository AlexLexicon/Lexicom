using Lexicom.DependencyInjection.Primitives.For.UnitTesting.Exceptions;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting.Extensions;
public static class RandomProviderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Seed(this IRandomProvider randomProvider, int seed)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Seed(seed);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Set(this IRandomProvider randomProvider, int next)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Set(next);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Set(this IRandomProvider randomProvider, long next64)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Set(next64);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Set(this IRandomProvider randomProvider, float nextSingle)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Set(nextSingle);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Set(this IRandomProvider randomProvider, double nextDouble)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Set(nextDouble);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Enqueue(this IRandomProvider randomProvider, int next)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Enqueue(next);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Enqueue(this IRandomProvider randomProvider, long next64)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Enqueue(next64);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Enqueue(this IRandomProvider randomProvider, float nextSingle)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Enqueue(nextSingle);
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NonTestProviderExtensionException{IRandomProvider, TestRandomProvider}"/>
    public static void Enqueue(this IRandomProvider randomProvider, double nextDouble)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        TestRandomProvider testRandomProvider = GetTestRandomProvider(randomProvider);

        testRandomProvider.Enqueue(nextDouble);
    }

    private static TestRandomProvider GetTestRandomProvider(IRandomProvider randomProvider)
    {
        if (randomProvider is not TestRandomProvider testRandomProvider)
        {
            throw new NonTestProviderExtensionException<IRandomProvider, TestRandomProvider>();
        }

        return testRandomProvider;
    }
}