using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting.Extensions;
public static class TestDependencyInjectionPrimitivesServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ITestDependencyInjectionPrimitivesServiceBuilder AddTestTimeProvider(this ITestDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<ITimeProvider, TestTimeProvider>();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static ITestDependencyInjectionPrimitivesServiceBuilder AddTestGuidProvider(this ITestDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGuidProvider, TestGuidProvider>();

        return builder;
    }
}
