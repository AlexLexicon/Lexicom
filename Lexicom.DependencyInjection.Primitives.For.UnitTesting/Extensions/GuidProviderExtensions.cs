using Lexicom.DependencyInjection.Primitives.For.UnitTesting.Exceptions;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting.Extensions;
public static class GuidProviderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void Set(this IGuidProvider guidProvider, Guid guid)
    {
        ArgumentNullException.ThrowIfNull(guidProvider);

        TestGuidProvider testGuidProvider = GetTestGuidProvider(guidProvider);

        testGuidProvider.Set(guid);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void Enqueue(this IGuidProvider guidProvider, Guid guid)
    {
        ArgumentNullException.ThrowIfNull(guidProvider);

        TestGuidProvider testGuidProvider = GetTestGuidProvider(guidProvider);

        testGuidProvider.Enqueue(guid);
    }

    private static TestGuidProvider GetTestGuidProvider(IGuidProvider guidProvider)
    {
        if (guidProvider is not TestGuidProvider testGuidProvider)
        {
            throw new NonTestProviderExtensionException<IGuidProvider, TestGuidProvider>();
        }

        return testGuidProvider;
    }
}
