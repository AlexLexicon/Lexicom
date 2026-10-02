using Lexicom.Supports.Testing;

namespace Lexicom.DependencyInjection.Primitives.For.Testing.Extensions;

public static class TestingServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddPrimitives(this ITestingServiceBuilder builder, Action<ITestDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new TestDependencyInjectionPrimitivesServiceBuilder(builder.Services));

        return builder;
    }
}
